using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Exceptions
{
    public class FileSizeException : Exception
    {
        public FileSizeException()
        {
        }
        public FileSizeException(string message) : base(message)
        {
        }

    }
}