using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentApplication.Models
{
    public class FeedbackService
    {
         public int FeedbackModelId { get; set; }
        public FeebackModel FeedbackModel { get; set; }

        public int ServiceModelId { get; set; }
        public ServiceModel ServiceModel { get; set; }
    }
}