using System.IO;
using System.Runtime.InteropServices;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class SendFileToQueueStorage
    {
        private BlobStorageParams blobParams;
        public int Execute(string fdatas, string fpars, ref string fouts)
        {
            try
            {
                blobParams = new BlobStorageParams(fpars);
                TransmitFileToQueueStorage();
                fouts = $"Result=Wysłano plik {blobParams.localFileName} do kolejki {blobParams.queueName}";
                return 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return -1;
        }
        private void TransmitFileToQueueStorage()
        {
            QueueClient queueClient = new QueueClient(blobParams.connectionString, blobParams.queueName);
            var content = File.ReadAllText(blobParams.localFileName);
            queueClient.SendMessage(content);
            if (blobParams.deleteFile) File.Delete(blobParams.localFileName);
        }
    }
    [ComVisible(true)]
    public class PeekFileFromQueueStorage
    {
        private BlobStorageParams blobParams;
        public int Execute(string fdatas, string fpars, ref string fouts)
        {
            try
            {
                blobParams = new BlobStorageParams(fpars);
                GetFileFromQueueStorage();
                fouts = $"Result=Wysłano plik {blobParams.localFileName} do kolejki {blobParams.queueName}";
                return 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return -1;
        }
        private void GetFileFromQueueStorage()
        {
            QueueClient queueClient = new QueueClient(blobParams.connectionString, blobParams.queueName);
            var response = queueClient.PeekMessage().Value;
            File.WriteAllText(blobParams.localFileName, response.Body.ToString());
        }
    }
    [ComVisible(true)]
    public class ReceiveFileFromQueueStorage
    {
        private BlobStorageParams blobParams;
        public int Execute(string fdatas, string fpars, ref string fouts)
        {
            try
            {
                blobParams = new BlobStorageParams(fpars);
                PopFileFromQueueStorage();
                fouts = $"Result=Wysłano plik {blobParams.localFileName} do kolejki {blobParams.queueName}";
                return 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return -1;
        }
        private void PopFileFromQueueStorage()
        {
            QueueClient queueClient = new QueueClient(blobParams.connectionString, blobParams.queueName);
            QueueMessage response = queueClient.ReceiveMessage().Value;
            File.WriteAllText(blobParams.localFileName, response.Body.ToString());
            if (blobParams.deleteFile)
                queueClient.DeleteMessage(response.MessageId, response.PopReceipt);
        }
    }
}
