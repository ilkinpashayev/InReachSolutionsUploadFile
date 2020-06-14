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
        
        public void UploadFile(uploadFormObject uploadFormObj)
        {
            var result = 0;
            if (!ModelState.IsValid)
            {
                       
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
                        _SendEmail.Send(_UploadFileResult.PreSignedURL, uploadFormObj.file.FileName);
                            
                        result = 1;
                    }
                }

                
            }

        }
    }
}