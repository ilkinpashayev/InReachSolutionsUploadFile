using Amazon.Glacier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Domain
{
    public class UploadFileResult
    {
        public StatusCodes StatusCode { get; set; }
        public string PreSignedURL { get; set; }
        public UploadFileResult()
        {
            StatusCode = StatusCodes.Empty;
            PreSignedURL = "";
        }
    }
}