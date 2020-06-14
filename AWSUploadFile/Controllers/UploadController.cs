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
            if (!ModelState.IsValid)
            {
                       
            }
            else
            {
                var _amazonS3 = new AmazonS3();
                var _amazonClient = _amazonS3.CreateClient();
                ServerMapPath = Server.MapPath("~/Content/images");

                UploadFileObject _uploadFileObject = new UploadFileObject();
                _uploadFileObject.ServerMapPath = ServerMapPath;
                _uploadFileObject.file = uploadFormObj.file;
                _uploadFileObject.AmazonClient = _amazonClient._AmazonS3Client;

                var _UploadFileResult = _amazonS3.UploadFile(_uploadFileObject);

                    



            }    
            
        }
    }
}