using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Exceptions
{
    [Serializable]
    public class AmazonClientBucketException : Exception
    {
        public AmazonClientBucketException()
        {
        }
        public AmazonClientBucketException(string message) : base(message)
        {
        }
    }
}
