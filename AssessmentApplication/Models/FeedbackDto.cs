using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentApplication.Models
{
    public class FeedbackDto
    {
        public int Id {get;set;}
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int Age { get; set; }
        public string TypeFeedback { get; set; }
        public string AdditionalComment { get; set; }
        public IFormFile? FileUpload { get; set; }
        public List<int> SelectedServices { get; set; } // For selected services
        }
}