using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipApplier.Services.Requests
{
    public class JobUpdateRequest : JobRequest
    {
        [Required]
        public int Id { get; set; }
        public DateTime DateApplied { get; set; }
        public bool Archived { get; set; }
    }
}
