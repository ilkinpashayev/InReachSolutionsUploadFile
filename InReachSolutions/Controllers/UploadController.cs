using Amazon.DeviceFarm.Model;
using AWSUploadFile.Services;
using InReachSolutions.Domain;
using InReachSolutions.Exceptions;
using InReachSolutions.Helper;
using InReachSolutions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InReachSolutions.Controllers
{
    public class UploadController : Controller
    {
        private string ServerMapPath { get; set; }
        private AmazonService amazonService = new AmazonService();
        private UploadRequestValidator validator = new UploadRequestValidator();
        private EmailService emailService = new EmailService();
        

        public UploadController()
        {
            var aw = ConfigurationService.Instance.AWSAccessKey;
        }
        [HttpGet]
        public ActionResult Upload()
        {

            return View();
        }
        [HttpPost]

        public ActionResult UploadFile(UploadRequest uploadFormRequest)
        {
            ServerMapPath = Server.MapPath("~/Content/images");
            try
            {
                validator.Validate(ModelState,uploadFormRequest);
                var amazonClient = amazonService.CreateClient();
                var uploadFileModel = new UploadFileModel(uploadFormRequest.file, amazonClient.AmazonClient, ServerMapPath);
                var uploadResponse = amazonService.UploadFile(uploadFileModel);
                emailService.PrepareEmail(uploadResponse.PreSignedURL, uploadFormRequest.file.FileName);
                emailService.Send(uploadFormRequest.Email);
                ViewBag.Message = "FIle had been uploaded successfully.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return View("Upload");
        }
    }
}