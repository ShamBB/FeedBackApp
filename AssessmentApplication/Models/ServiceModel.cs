using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentApplication.Models
{
    public class ServiceModel
    {
         public int Id { get; set; }
        public string Name { get; set; }
        // Navigation property for the many-to-many relationship
        public ICollection<FeedbackService> FeedbackServices { get; set; } = new List<FeedbackService>();
    }
}