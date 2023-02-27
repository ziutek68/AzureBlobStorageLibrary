using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
        public static int ShowContainerWarning(int result, BlobStorageParams blobParams)
        {
            if (blobParams.messsageType != BlobStorageParams.MsgType.mtNone)
            {
                switch (result)
                {
                    case 1:
                        MessageBox.Show($"Brak kontenera {blobParams.containerName} na Azure!", "AzureBlobStorageLibrary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 2:
                        MessageBox.Show($"Brak pliku {blobParams.localFileName} w kontenerze {blobParams.containerName}!", "AzureBlobStorageLibrary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show($"Inny błąd podczas komunikacji z Azure!", "AzureBlobStorageLibrary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            return result;
        }
        public static int ShowQueueWarning(int result, BlobStorageParams blobParams)
        {
            if (blobParams.messsageType != BlobStorageParams.MsgType.mtNone)
            {
                switch (result)
                {
                    case 1:
                        MessageBox.Show($"Brak kolejki {blobParams.queueName} na Azure!", "AzureBlobStorageLibrary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 2:
                        MessageBox.Show($"Kolejka {blobParams.queueName} jest pusta!", "AzureBlobStorageLibrary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show($"Inny błąd podczas komunikacji z Azure!", "AzureBlobStorageLibrary", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            return result;
        }
    }
}