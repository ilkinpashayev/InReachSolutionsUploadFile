using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Services
{
    [Serializable]
    public class UploadFileValidateException : Exception
    {
        public UploadFileValidateException()
        {
        }
        public UploadFileValidateException(string message): base(message)
        {

        }
    }
}