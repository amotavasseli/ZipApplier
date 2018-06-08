using System.Collections.Generic;
using ZipApplier.Services.Domains;
using ZipApplier.Services.Requests;

namespace ZipApplier.Services.Services
{
    public interface IJobsService
    {
        List<Job> GetAllJobs();
        Job GetById(int id);
        void Update(JobUpdateRequest req);
        void Delete(int id);
    }
}