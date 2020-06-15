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
        private UploadRequestValidator validate = new UploadRequestValidator();
        public UploadController()
        {
            
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
                validate.Validate(ModelState,uploadFormRequest);
                var amazonClient = amazonService.CreateClient();
                var rr = new UploadFileModel(uploadFormRequest.file, amazonClient.AmazonClient, ServerMapPath);
                var _UploadFileResult = amazonService.UploadFile(rr);
            }
            catch(UploadFileValidateException ex)
            {
                ViewBag.Message = ex.Message;
            }
            catch (AmazonServiceClientException ex)
            {
                ViewBag.Message = ex.Message;
            }

           

            return View("Upload");
            /*            
            

            //if (amazonClient.StausCode == StatusCodes.AWSConnectionSuccess)
           // {
                var uploadFileModel = new UploadFileModel(uploadFormRequest.file, amazonClient.AmazonClient, ServerMapPath);
                
                var _UploadFileResult = amazonService.UploadFile(_uploadFileObject);
                if (_UploadFileResult._Result == 1)
                {
                    SendEmail _SendEmail = new SendEmail();
                    _SendEmail.Send(_UploadFileResult.PreSignedURL, uploadFormObj.file.FileName, uploadFormObj.Email);

                    result = 1;
                }
           // }

            if (result != 1)
            {
                ViewBag.Message = ViewBag.Message + System.Environment.NewLine +
                    "Error occured while uploading and sending email notification.";
            }
            else
            {
                ViewBag.Message = "File has been upload to AWS successfully";
            }


            return View("Upload");
            //("NameOfView", Model);
            */
        }
    }
}