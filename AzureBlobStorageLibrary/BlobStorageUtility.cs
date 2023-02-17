﻿using System.IO;
using System.Linq;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobStorageLibrary
{
    public static class BlobStorageUtility
    {
        public static BlobClient GetBlobClient(BlobStorageParams blobParams, bool create)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(blobParams.connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobParams.containerName);
            if (containerClient == null)
            {
                if (create)
                    containerClient = blobServiceClient.CreateBlobContainer(blobParams.containerName);
                else
                    return null;
            }
            var fileName = Path.GetFileName(blobParams.localFileName);
            return containerClient.GetBlobClient(fileName);
        }
        public static Azure.Pageable<BlobItem> GetBlobs(BlobStorageParams blobParams)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(blobParams.connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobParams.containerName);
            if (containerClient == null) return null;
            return containerClient.GetBlobs();
        }
        public static BlobClient GetOldestBlob(BlobStorageParams blobParams)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(blobParams.connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobParams.containerName);
            if (containerClient == null) return null;
            var blobList = containerClient.GetBlobs();
            var oldestBlob = blobList.OrderBy(blob => blob.Properties.CreatedOn).ToList().FirstOrDefault();
            if (oldestBlob == null) return null;
            return containerClient.GetBlobClient(oldestBlob.Name);
        }
    }
}