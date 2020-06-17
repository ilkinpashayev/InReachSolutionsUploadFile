using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.Web;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using InReachSolutions.Controllers;
using InReachSolutions.Domain;
using InReachSolutions.Exceptions;

namespace InReachSolutions.Helper
{
    public class AmazonService
    {
        private static readonly string bucketName = ConfigurationManager.AppSettings["BucketName"];
        private static readonly string accesskey = ConfigurationManager.AppSettings["AWSAccessKey"];
        private static readonly string secretkey = ConfigurationManager.AppSettings["AWSSecretKey"];
        //private static RegionEndpoint bucketRegion =    RegionEndpoint.GetBySystemName(ConfigurationManager.AppSettings["RegionEndpoint"]);
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest1;

        public AmazonService()
        {
        }

        private void CheckBucket(AmazonS3Client amazonS3Client)
        {
            try
            {
                var listBuckets = amazonS3Client.ListBuckets();

                foreach (S3Bucket bucket in listBuckets.Buckets)
                {
                    if (bucket.BucketName == bucketName)
                    {
                        return;
                    }
                }
                throw new AmazonClientBucketException($"Couldn't find bucket: {bucketName}");
            }
            catch (Exception ex)
            {
                throw new AmazonClientBucketException("Couldn't connect to Amazon S3 Client. "+ex.Message);
                
            }

        }
        public AmazonServiceClientResponse CreateClient()
        {
            try
            {
                var amazonServiceClientResponse = new AmazonServiceClientResponse();
                var amazonS3Client = new AmazonS3Client(accesskey, secretkey, bucketRegion);
                amazonServiceClientResponse.StatusCode = StatusCodes.AWSConnectionSuccess;
                amazonServiceClientResponse.AmazonClient = amazonS3Client;
                var listBuckets = amazonS3Client.ListBuckets();
                //CheckBucket(amazonS3Client);
                return amazonServiceClientResponse;
            }
            catch(AmazonClientBucketException ex)
            {
                throw new AmazonServiceClientException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new AmazonServiceClientException("Couldn't connect to Amazon S3 Client. "+ ex.Message);
            }
            return null;
        }
        
        private TransferUtilityUploadRequest SaveFile(UploadFileModel uploadFileModel)
        {
            try
            {
                var keyName = uploadFileModel.File.FileName;
                var filePath = Path.Combine(uploadFileModel.ServerMapPath, keyName);
                uploadFileModel.File.SaveAs(filePath);
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = filePath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 4096, // 6 MB.  
                    Key = keyName,
                    CannedACL = S3CannedACL.PublicRead
                };
                return fileTransferUtilityRequest;
            }
            catch(Exception ex)
            {
                throw new SaveFileException("An error occured while saving file or setting file settings.");
            }
            return null;
        }
        public string GetPreSignedUrl(UploadFileModel uploadFileModel)
        {
            try
            {
                var expiryUrlRequest = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = uploadFileModel.File.FileName,
                    Expires = DateTime.Now.AddMinutes(5)
                };
                var fileurl = uploadFileModel.AmazonClient.GetPreSignedURL(expiryUrlRequest);
                return fileurl;
            }
            catch(Exception ex)
            {
                throw new GetPreSignedUrlException("An error occured while getting presigned url for uploaded file.");
            }
            return null;
        }

        public UploadFileResult UploadFile(UploadFileModel uploadFileModel)
        {
            string urlfile = "";
            string filePath;
            try
            {
                var fileTransferUtility = new TransferUtility(uploadFileModel.AmazonClient);
                var fileTransferUtilityRequest = SaveFile(uploadFileModel);
                fileTransferUtility.Upload(fileTransferUtilityRequest);
                var preSignedURL = GetPreSignedUrl(uploadFileModel);
                var uploadFileResult = new UploadFileResult(StatusCodes.AWSUploadFileSuccess,preSignedURL);
                fileTransferUtility.Dispose();
                return uploadFileResult;
            }
            catch(GetPreSignedUrlException ex)
            {

            }
            catch (Exception ex)
            {
                throw new UploadFileException("An error occured while uploading file");
            }
            return null ;
        }
        
}
}