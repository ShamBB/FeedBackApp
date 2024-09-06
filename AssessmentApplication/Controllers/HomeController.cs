using AssessmentApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AssessmentApplication.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext appDbContext;

        public HomeController(ApplicationDbContext sc)
        {
            appDbContext = sc; 
        }

        public IActionResult Index()
        {
            var feedbackList = appDbContext.FeebackModels
                .Include(f => f.FeedbackServices)
                    .ThenInclude(fs => fs.ServiceModel)
                .Include(f => f.DocumentAttachments)
                .ToList();

            return View(feedbackList);
        }

        public IActionResult Create()
        {
            SetServicesInViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult Create(FeedbackDto feedbackDto)
        {
            if (ModelState.IsValid)
            {
                var feedback = new FeebackModel
                {
                    Name = feedbackDto.Name,
                    Email = feedbackDto.Email,
                    Age = feedbackDto.Age,
                    TypeFeedback = feedbackDto.TypeFeedback,
                    AdditionalComment = feedbackDto.AdditionalComment,
                    FeedbackServices = new List<FeedbackService>()
                };

                HandleFileUpload(feedbackDto, feedback);
                HandleSelectedServices(feedbackDto, feedback);

                appDbContext.FeebackModels.Add(feedback);
                appDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            SetServicesInViewBag();
            return View(feedbackDto);
        }

        public IActionResult Update(int id)
        {
            var feedback = appDbContext.FeebackModels
                .Include(f => f.FeedbackServices)
                .ThenInclude(fs => fs.ServiceModel)
                .FirstOrDefault(f => f.Id == id);

            if (feedback == null)
            {
                return NotFound();
            }

            var feedbackDto = new FeedbackDto
            {
                Name = feedback.Name,
                Email = feedback.Email,
                Age = feedback.Age,
                TypeFeedback = feedback.TypeFeedback,
                AdditionalComment = feedback.AdditionalComment,
                SelectedServices = feedback.FeedbackServices.Select(fs => fs.ServiceModelId).ToList(),
                Id = feedback.Id,
                FileUpload = null
            };

            SetServicesInViewBag();
            ViewBag.ExistingFileName = appDbContext.AttachmentModels
                .Where(a => a.FeedbackModelId == feedback.Id)
                .Select(a => a.FileName)
                .FirstOrDefault();
            return View(feedbackDto);
        }

        [HttpPost]
        public IActionResult Update(FeedbackDto feedbackDto, int id)
        {
            if (ModelState.IsValid)
            {
                var feedback = appDbContext.FeebackModels
                    .Include(f => f.FeedbackServices)
                    .FirstOrDefault(f => f.Id == id);

                if (feedback == null)
                {
                    return NotFound();
                }

                feedback.Name = feedbackDto.Name;
                feedback.Email = feedbackDto.Email;
                feedback.Age = feedbackDto.Age;
                feedback.TypeFeedback = feedbackDto.TypeFeedback;
                feedback.AdditionalComment = feedbackDto.AdditionalComment;

                HandleFileUpload(feedbackDto, feedback);
                feedback.FeedbackServices.Clear();
                HandleSelectedServices(feedbackDto, feedback);

                appDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            SetServicesInViewBag();
            return View(feedbackDto);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var feedback = appDbContext.FeebackModels
                .Include(f => f.DocumentAttachments)
                .Include(f => f.FeedbackServices)
                .FirstOrDefault(f => f.Id == id);

            if (feedback == null)
            {
                return NotFound();
            }

            appDbContext.FeebackModels.Remove(feedback);
            if (feedback.DocumentAttachments != null)
            {
                appDbContext.AttachmentModels.Remove(feedback.DocumentAttachments);
            }

            appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult DownloadFile(int id)
        {
            var attachment = appDbContext.AttachmentModels.FirstOrDefault(a => a.FeedbackModelId == id);
            if (attachment == null)
            {
                return NotFound();
            }
            return File(attachment.FileData, "application/octet-stream", attachment.FileName);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetServicesInViewBag()
        {
            var services = appDbContext.ServiceModels.ToList();
            ViewBag.Services = services;
        }

        private void HandleFileUpload(FeedbackDto feedbackDto, FeebackModel feedback)
        {
            if (feedbackDto.FileUpload != null && feedbackDto.FileUpload.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    feedbackDto.FileUpload.CopyTo(memoryStream);
                    var attachment = new AttachmentModel
                    {
                        FileData = memoryStream.ToArray(),
                        FileName = feedbackDto.FileUpload.FileName
                    };
                    feedback.DocumentAttachments = attachment;
                }
            }
        }

        private void HandleSelectedServices(FeedbackDto feedbackDto, FeebackModel feedback)
        {
            if (feedbackDto.SelectedServices != null && feedbackDto.SelectedServices.Count > 0)
            {
                foreach (var serviceId in feedbackDto.SelectedServices)
                {
                    feedback.FeedbackServices.Add(new FeedbackService
                    {
                        ServiceModelId = serviceId
                    });
                }
            }
        }
    }
}