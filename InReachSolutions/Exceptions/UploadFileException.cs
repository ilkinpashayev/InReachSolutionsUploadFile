using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Exceptions
{
    public class UploadFileException : Exception
    {
        public UploadFileException()
        {
        }
        public UploadFileException(string message) : base(message)
        {
        }

    }
}