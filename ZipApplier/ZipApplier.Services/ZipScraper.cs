using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipApplier.Services.Requests;

namespace ZipApplier.Services
{
    public class ZipScraper
    {
        public List<JobRequest> Scrape()
        {
            string url = "https://www.ziprecruiter.com/candidate/search?search=.Net+Developer&location=Los+Angeles%2C+CA&days=10&radius=25&refine_by_salary=&refine_by_tags=&refine_by_title=&refine_by_org_name=";

            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--incognito");
            options.AddArgument("--ignore-certificate-errors");
            IWebDriver chromeDriver = new ChromeDriver("C:\\repos\\github\\ZipApplier\\ZipApplier\\ZipApplier.Services\\bin\\Release", options);
            chromeDriver.Url = url;
            var html = chromeDriver.PageSource;
            var parser = new HtmlParser();
            var doc = parser.Parse(html);

            List<JobRequest> jobs = new List<JobRequest>();
            captureListings(doc);

            //Below finds the total number of listings returned by the search.
            // 1. locate element that contains the text with the listing. 
            // 2. cut the number out of the string.
            // 3. convert the string into an int. 
            var headline = doc.QuerySelector("title").TextContent;
            string stringTotal = headline.Substring(0, headline.IndexOf("+"));
            if (stringTotal.Contains(","))
                stringTotal = stringTotal.Replace(",", "");
            int total = Convert.ToInt32(stringTotal);
            //ZipRecruiter lists 20 job postings on each page. 
            //Below will determine how many pages need to be checked to work around pagination. 
            int pages = 1;
            int extraPage = 0;

            if (total > 20)
                pages = (int)Math.Floor((decimal)(total / 20));

            if (total % 20 != 0)
                extraPage = 1;

            pages += extraPage;

            if (pages > 1)
            {
                for (int p = 2; p <= pages; p++)
                {
                    chromeDriver.Navigate().GoToUrl(url + "&page=" + p);
                    html = chromeDriver.PageSource;
                    parser = new HtmlParser();
                    doc = parser.Parse(html);
                    captureListings(doc);
                }
            }
            // Inline function will be called to parse html for all necessary data. 
            void captureListings(IHtmlDocument document)
            {
                var listings = document.QuerySelectorAll("article.t_job_result");
                for (int i = 0; i < listings.Length; i++)
                {
                    var listing = listings[i];
                    var title = listing.QuerySelector("h2.job_title").TextContent;
                    // avoid jobs with the following titles: 
                    if (!title.Contains("Senior")
                        && !title.Contains("Sr")
                        && !title.Contains("Lead")
                        && !title.Contains("Principal")
                        && !title.Contains("Java")
                        && !title.Contains("Clearance")
                        && !title.Contains("Graphics")
                        && !title.Contains("Android")
                        && !title.Contains("iOS")
                        && !title.Contains("Wordpress")
                        && !title.Contains("WordPress")
                        && !title.Contains("PHP")
                        && !title.Contains("Architect")
                        && !title.Contains("Ruby")
                        && !title.Contains("Manager")
                        && !title.Contains("Design")
                        && !title.Contains("Python")
                        && !title.Contains("HTML")
                        && !title.Contains("CSS")
                        && !title.Contains("Salesforce")
                        && !title.Contains("SENIOR")
                        && !title.Contains("Analyst")
                        //include jobs that contain the following titles
                        && title.Contains(".NET") //this needs to be changed with each search
                        )
                    {
                        var id = listing.QuerySelector("span.just_job_title").GetAttribute("data-job-id");

                        JobRequest job = new JobRequest();
                        job.JobId = id;
                        job.Title = title;
                        job.Company = listing.QuerySelector(".t_org_link").TextContent;
                        job.Location = listing.QuerySelector(".t_location_link").TextContent;
                        string link = listing.QuerySelector("a.t_job_link").GetAttribute("href");
                        link = link.Substring(0, link.IndexOf("?"));
                        job.Url = link;
                        job.Description = listing.QuerySelector(".job_snippet").TextContent;
                        if (listing.QuerySelector("button").ClassList.Contains("job_apply"))
                            job.QuickApply = true;
                        jobs.Add(job);
                    }
                }

            }

            return jobs;
        }
    }
}
