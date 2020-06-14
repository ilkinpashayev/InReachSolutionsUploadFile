using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWSUploadFile.Domain
{
    public class UploadFileResult
    {
        public Int32 _Result { get; set; }
        public string PreSignedURL { get; set; }
    }
}