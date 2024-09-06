using AssessmentApplication.Models;

public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<FeebackModel> Feedbacks { get; set; } // Navigation property
    }