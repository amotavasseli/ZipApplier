using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipApplier.Services.Interfaces;
using ZipApplier.Services.Requests;

namespace ZipApplier.Services.Services
{
    public class ZipScraperService : IZipScraperService
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ZipApplierConnection"].ConnectionString;

        public List<JobRequest> PostScrapedJobs()
        {
            List<JobRequest> jobs = new ZipScraper().Scrape();
            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                for(int i = 0; i < jobs.Count; i++)
                {
                    JobRequest job = jobs[i];
                    try
                    {
                        //Post new job into the database
                        SqlCommand cmd = new SqlWrapper().Wrapper("ZipApplier_insert", con);
                        cmd.Parameters.AddWithValue("@job_id", job.JobId);
                        cmd.Parameters.AddWithValue("@title", job.Title);
                        cmd.Parameters.AddWithValue("@url", job.Url);
                        cmd.Parameters.AddWithValue("@company", job.Company);
                        cmd.Parameters.AddWithValue("@description", job.Description);
                        cmd.Parameters.AddWithValue("@location", job.Location);
                        cmd.Parameters.AddWithValue("@quick_apply", job.QuickApply);
                        cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                    } catch(SqlException exp) when (exp.Number == 2601)
                    {
                        //if job posting already exists in the database, ignore and continue.
                        continue;
                    }
                }
                return jobs;
            }
        }
    }
}
