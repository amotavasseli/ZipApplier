using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ZipApplier.Services.Domains;
using ZipApplier.Services.Interfaces;
using ZipApplier.Services.Requests;
using ZipApplier.Services.Services;

namespace ZipApplier.Web.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class JobsController : ApiController
    {
        readonly IZipScraperService zipService;
        readonly IJobsService jobsService;
        public JobsController(IZipScraperService zipService, IJobsService jobsService)
        {
            this.zipService = zipService;
            this.jobsService = jobsService;
        }

        [HttpPost, Route("api/zipscraper")]
        public HttpResponseMessage PostScrapedJobs()
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            List<JobRequest> jobs = zipService.PostScrapedJobs();
            return Request.CreateResponse(HttpStatusCode.OK, jobs);
        }

        [HttpGet, Route("api/jobs")]
        public HttpResponseMessage GetAll()
        {
            List<Job> jobs = jobsService.GetAllJobs();
            return Request.CreateResponse(HttpStatusCode.OK, jobs);
        }

        [HttpGet, Route("api/jobs/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            Job job = jobsService.GetById(id);
            return Request.CreateResponse(HttpStatusCode.OK, job);
        }
    }
}