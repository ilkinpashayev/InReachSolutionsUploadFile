using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWSUploadFile.Domain
{
    public class AmazonS3ClientResponse
    {
        public Int32 _Result { get; set; }
        public AmazonS3Client _AmazonS3Client { get; set; }
    }
}