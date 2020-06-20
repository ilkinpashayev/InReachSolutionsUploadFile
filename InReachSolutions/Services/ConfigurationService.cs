using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AWSUploadFile.Services
{
    public sealed class ConfigurationService
    {
        private ConfigurationService()
        {
        }

        private static ConfigurationService instance = null;

        public static ConfigurationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigurationService();
                }
                return instance;
            }
        }

        private string awsAcessKey { get; set; }

        public string AWSAccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AWSAccessKey"];
            }
        }

        private string awsSecretKey { get; set; }

        public string AWSSecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AWSSecretKey"];
            }
        }

        private string bucketName { get; set; }

        public string BucketName
        {
            get
            {
                return ConfigurationManager.AppSettings["BucketName"];
            }
        }

        private string regionEndpoint { get; set; }
        public string RegionEndpoint
        {
            get
            {
                return ConfigurationManager.AppSettings["BucketName"];
            }
        }

        private string emailFrom { get; set; }
        public string EmailFrom
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailFrom"];
            }
        }

        private string emailFromName { get; set; }
        public string EmailFromName
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailFromName"];
            }
        }

        private string emailPassword { get; set; }
        public string EmailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailPassword"];
            }
        }

        private string emailHost { get; set; }
        public string EmailHost
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailHost"];
            }
        }

        private string emailPort { get; set; }
        public string EmailPort
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailPort"];
            }
        }

        private string emailEnableSsl { get; set; }
        public string EmailEnableSsl
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailEnableSsl"];
            }
        }

        private string emailUseDefaultCredentials { get; set; }
        public string EmailUseDefaultCredentials
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailUseDefaultCredentials"];
            }
        }

        private string emailNetwork { get; set; }
        public string EmailNetwork
        {
            get
            {
                return ConfigurationManager.AppSettings["EmailNetwork"];
            }
        }
    }
 }
