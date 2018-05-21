using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ZipApplier.Services.Interfaces;
using ZipApplier.Services.Requests;

namespace ZipApplier.Web.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class JobsController : ApiController
    {
        readonly IZipScraperService zipService; 
        public JobsController(IZipScraperService zipService)
        {
            this.zipService = zipService;
        }

        [HttpPost, Route("api/zipscraper")]
        public HttpResponseMessage PostScrapedJobs()
        {
            List<Job> jobs = zipService.PostScrapedJobs();
            return Request.CreateResponse(HttpStatusCode.OK, jobs);
        }
    }
}