using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipApplier.Services.Domains
{
    public class Job
    {
        public int Id { get; set; }
        public string JobId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? DateApplied { get; set; }
        public bool Archived { get; set; }
        public bool QuickApply { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
