using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Exceptions
{
    public class SendEmailException : Exception
    {
        public SendEmailException()
        {
        }
        public SendEmailException(string message) : base(message)
        {
        }
    }
}