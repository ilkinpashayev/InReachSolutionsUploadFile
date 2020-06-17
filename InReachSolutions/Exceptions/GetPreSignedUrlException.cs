using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Exceptions
{
    public class GetPreSignedUrlException : Exception
    {
        public GetPreSignedUrlException()
        {
        }
        public GetPreSignedUrlException(string message) : base(message)
        {
        }

    }
}