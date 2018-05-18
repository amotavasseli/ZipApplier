using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipApplier.ConsoleApp
{
    public class Job
    {
        public string JobId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PostDate { get; set; }
        public DateTime DateApplied { get; set; }
        public bool Archived { get; set; }
    }
}
