using AWSUploadFile.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AWSUploadFile.Controllers
{
    public class UploadController : Controller
    {
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

            
        }
    }
}