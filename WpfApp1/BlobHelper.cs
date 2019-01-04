using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftEventsPublisher
{
    class BlobHelper
    {
        private string storageConnectionString = ConfigurationManager.AppSettings["blobConnectionString"].ToString();
        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;
        private CloudBlobContainer blobContainer;

        public BlobHelper()
        {
            storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            blobClient = storageAccount.CreateCloudBlobClient();
            blobContainer = blobClient.GetContainerReference("");
        }

        public void UploadLogs()
        {
            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference("EventPublisherLogs");

            using (var fileStream = System.IO.File.OpenRead(@"path\myfile"))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
    }
}
