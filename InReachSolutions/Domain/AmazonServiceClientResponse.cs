using Amazon.Glacier;
using Amazon.S3;

namespace InReachSolutions.Domain
{
    public class AmazonServiceClientResponse
    {
        public StatusCodes StatusCode { get; set; }
        public AmazonS3Client AmazonClient { get; set; }
        public AmazonServiceClientResponse()
        {
            this.StatusCode = StatusCodes.Empty;
            this.AmazonClient = null;
        }
    }
}