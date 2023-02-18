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
            int res = -1;
            try
            {
                blobParams = new BlobStorageParams(fpars);
                res = GetFileFromQueueStorage();
                if (res == 0)
                    fouts = $"Result=Jest plik {blobParams.localFileName} z kolejki {blobParams.queueName}";
                return 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return res;
        }
        private int GetFileFromQueueStorage()
        {
            QueueClient queueClient = new QueueClient(blobParams.connectionString, blobParams.queueName);
            if (!queueClient.Exists()) return 1;
            var response = queueClient.PeekMessage().Value;
            if (response == null) return 2;
            File.WriteAllText(blobParams.localFileName, response.Body.ToString());
            return 0;
        }
    }
    [ComVisible(true)]
    public class ReceiveFileFromQueueStorage
    {
        private BlobStorageParams blobParams;
        public int Execute(string fdatas, string fpars, ref string fouts)
        {
            int res = -1;
            try
            {
                blobParams = new BlobStorageParams(fpars);
                res = PopFileFromQueueStorage();
                if (res == 0)
                    fouts = $"Result=Ściągnięto plik {blobParams.localFileName} z kolejki {blobParams.queueName}";
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return res;
        }
        private int PopFileFromQueueStorage()
        {
            QueueClient queueClient = new QueueClient(blobParams.connectionString, blobParams.queueName);
            if (!queueClient.Exists()) return 1;
            QueueMessage response = queueClient.ReceiveMessage().Value;
            if (response == null) return 2;
            File.WriteAllText(blobParams.localFileName, response.Body.ToString());
            if (blobParams.deleteFile)
                queueClient.DeleteMessage(response.MessageId, response.PopReceipt);
            return 0;
        }
    }
}
