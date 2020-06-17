using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Exceptions
{
    public class SaveFileException : Exception
    {
        public SaveFileException()
        {
        }
        public SaveFileException(string message) : base(message)
        {
        }

    }
}