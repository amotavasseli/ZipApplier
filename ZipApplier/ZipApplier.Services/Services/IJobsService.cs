using System.Collections.Generic;
using ZipApplier.Services.Domains;

namespace ZipApplier.Services.Services
{
    public interface IJobsService
    {
        List<Job> GetAllJobs();
        Job GetById(int id);
    }
}