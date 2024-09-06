using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentApplication.Models
{
    public class AttachmentModel
    {
        public int Id { get; set; }
        public byte[] FileData { get; set; } // Store file as byte array
        public string FileName { get; set; } // Store original file name
        public int FeedbackModelId { get; set; }
        public FeebackModel FeedbackModel { get; set; }
    }
}