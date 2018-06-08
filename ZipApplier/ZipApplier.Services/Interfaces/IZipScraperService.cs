using System.Collections.Generic;
using ZipApplier.Services.Requests;

namespace ZipApplier.Services.Interfaces
{
    public interface IZipScraperService
    {
        List<JobRequest> PostScrapedJobs();
    }
}