using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Exceptions
{
    [Serializable]
    public class AmazonServiceClientException : Exception
    {

            public AmazonServiceClientException()
            {
            }
            public AmazonServiceClientException(string message) : base(message)
            {

            }
        
    }
}