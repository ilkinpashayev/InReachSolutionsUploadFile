using InReachSolutions.Domain;
using InReachSolutions.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InReachSolutions.Services
{
    public class UploadRequestValidator
    {
        public void Validate(ModelStateDictionary modelStateDictionary, UploadRequest uploadRequest)
        {
            if (!modelStateDictionary.IsValid)
            {
                throw new UploadFileValidateException("Please select file to upload and enter valid email address.");
            }

            if (uploadRequest.file.ContentLength <= 0)
            {
                throw new FileSizeException("File size must be greater than 0.");
            }
        }
    }
}