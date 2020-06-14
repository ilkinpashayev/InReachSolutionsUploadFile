using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Web;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using AWSUploadFile.Controllers;
using AWSUploadFile.Domain;


namespace AWSUploadFile.Helper
{
    public class AmazonS3
    {
        private  string bucketName = "";
        private  string bucketRegion = "";
        private  string accesskey = "";
        private string secretkey = "";
        private RegionEndpoint RegionEndPoint = null;


        public AmazonS3()
        {
            bucketName = ConfigurationManager.AppSettings["BucketName"];
            accesskey = ConfigurationManager.AppSettings["AWSAccessKey"];
            secretkey = ConfigurationManager.AppSettings["AWSSecretKey"];
            RegionEndPoint = RegionEndpoint.GetBySystemName(ConfigurationManager.AppSettings["RegionEndpoint"]);
            //RegionEndpoint bucketRegion = RegionEndpoint.USWest1;
    }

        public AmazonS3ClientResponse CreateClient()
        {
            AmazonS3Client _amazonS3Client= null ;
            AmazonS3ClientResponse _AmazonS3ClientResponse = new AmazonS3ClientResponse();
            _AmazonS3ClientResponse._Result = 1;
            _AmazonS3ClientResponse._AmazonS3Client = null;

            try
            {
                _amazonS3Client = new AmazonS3Client(this.accesskey, this.secretkey, this.RegionEndPoint);
                _AmazonS3ClientResponse._Result = 1;
                _AmazonS3ClientResponse._AmazonS3Client = _amazonS3Client;
            }
            catch(Exception ex)
            {
                _AmazonS3ClientResponse._Result = 1;
                _AmazonS3ClientResponse._AmazonS3Client = null;
            }

            return _AmazonS3ClientResponse;
        }

        public UploadFileResult UploadFile(UploadFileObject _UploadFileObject)
        {
           
           

            UploadFileResult _UploadFileResult = new UploadFileResult();
            _UploadFileResult._Result = -1;
            _UploadFileResult.PreSignedURL = "";

            var fileTransferUtility = new TransferUtility(s3Client);
            string urlfile = "";

 
            var bucketList = _UploadFileObject.AmazonClient.ListBuckets();
            string filePath;
 
            try
            {
                if (_UploadFileObject.file.ContentLength > 0)
                {
                    

                    var filename = Path.GetFileName(_UploadFileObject.file.FileName);
                    filePath = Path.Combine(_UploadFileObject.ServerMapPath, filename);
                    _UploadFileObject.file.SaveAs(filePath);

                    var keyName = Path.GetFileName(_UploadFileObject.file.FileName);

\
                    var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                    {
                        BucketName = bucketName,
                        FilePath = filePath,
                        StorageClass = S3StorageClass.StandardInfrequentAccess,
                        PartSize = 4096, // 6 MB.  
                        Key = keyName,
                        CannedACL = S3CannedACL.PublicRead
                    };


                    fileTransferUtilityRequest.Metadata.Add("param1", "Value1");
                    fileTransferUtilityRequest.Metadata.Add("param2", "Value2");
                    fileTransferUtility.Upload(fileTransferUtilityRequest);

                    var expiryUrlRequest = new GetPreSignedUrlRequest
                    {
                        BucketName = bucketName,
                        Key = keyName,
                        Expires = DateTime.Now.AddMinutes(5)
                    };


                    urlfile = s3Client.GetPreSignedURL(expiryUrlRequest);
                    _UploadFileResult._Result = 1;
                    _UploadFileResult.PreSignedURL = urlfile;
                        
                    fileTransferUtility.Dispose();
                }
                
            }

            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    _UploadFileResult._Result = -2;
                }
                else
                {
                    _UploadFileResult._Result = -3;
                }
            }

            return _UploadFileResult;
        }
}
}