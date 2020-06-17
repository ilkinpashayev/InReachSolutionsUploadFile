using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InReachSolutions.Domain
{
    public class UploadFileModel
    {
        public HttpPostedFileBase File { get; set; }
        public AmazonS3Client AmazonClient { get; set; }
        public string ServerMapPath { get; set; }
        public UploadFileModel(HttpPostedFileBase File, AmazonS3Client AmazonClient, string ServerMapPath)
        {
            this.File = File;
            this.AmazonClient = AmazonClient;
            this.ServerMapPath = ServerMapPath;
        }
    }
}