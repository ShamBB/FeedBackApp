using System.ComponentModel.DataAnnotations;

namespace AssessmentApplication.Models
{
    public class FeebackModel
    {

         public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string TypeFeedback { get; set; }
        public string AdditionalComment { get; set; }

        // Navigation property for multiple attachments
        public AttachmentModel DocumentAttachments { get; set; }

        // Navigation property for the many-to-many relationship
        public ICollection<FeedbackService> FeedbackServices { get; set; } = new List<FeedbackService>();
    }

    
}
