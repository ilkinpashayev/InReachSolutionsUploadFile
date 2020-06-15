using AWSUploadFile.Domain;
using AWSUploadFile.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AWSUploadFile.Controllers
{
    public class UploadController : Controller
    {
        private string ServerMapPath = "";
        // GET: Upload
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult UploadFile(uploadFormObject uploadFormObj)
        {
            var result = 0;
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Please select file to upload and enter valid email address.";

                
            }
            else
            {
                ServerMapPath = Server.MapPath("~/Content/images");
                var _amazonS3 = new AmazonS3();
                var _amazonClient = _amazonS3.CreateClient();
                if (_amazonClient._Result == 1)
                {
                    UploadFileObject _uploadFileObject = new UploadFileObject();
                    _uploadFileObject.ServerMapPath = ServerMapPath;
                    _uploadFileObject.file = uploadFormObj.file;
                    _uploadFileObject.AmazonClient = _amazonClient._AmazonS3Client;
                    var _UploadFileResult = _amazonS3.UploadFile(_uploadFileObject);
                    if (_UploadFileResult._Result==1)
                    {
                        SendEmail _SendEmail = new SendEmail();
                        _SendEmail.Send(_UploadFileResult.PreSignedURL, uploadFormObj.file.FileName,uploadFormObj.Email);
                            
                        result = 1;
                    }
                }

                if (result!=1)
                {
                    ViewBag.Message = ViewBag.Message+System.Environment.NewLine+
                        "Error occured while uploading and sending email notification.";
                }
                else
                {
                    ViewBag.Message = "File has been upload to AWS successfully";
                }
                
            }
            return View("Upload");
                //("NameOfView", Model);

        }
    }
}