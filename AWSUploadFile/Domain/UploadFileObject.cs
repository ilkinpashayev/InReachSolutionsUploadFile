using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AWSUploadFile.Domain
{
    public class UploadFileObject
    {
        public HttpPostedFileBase file { get; set; }
        public AmazonS3Client AmazonClient { get; set; }
        public string ServerMapPath { get; set; }
    }
}