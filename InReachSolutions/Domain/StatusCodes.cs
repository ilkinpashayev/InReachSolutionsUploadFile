using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Domain
{
    public enum StatusCodes
    {
        Empty = 0,
        OK = 1,
        AWSConnectionSuccess = 2,
        AWSUploadFileSuccess = 3,
    }
}