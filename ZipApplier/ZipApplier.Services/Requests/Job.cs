using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipApplier.Services.Requests
{
    public class Job
    {
        [Required]
        public string JobId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Company { get; set; }
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        public string PostDate { get; set; }
        [Required]
        public bool QuickApply { get; set; }
    }
}
