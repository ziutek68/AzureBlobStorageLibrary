using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
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
                if (blobParams.asyncProcess)
                {
                    Task.Run(() => TransmitFileToQueueStorage());
                    fouts = $"Result=Trwa wysyłanie {blobParams.localFileName} do kolejki {blobParams.containerName}";
                }
                else
                {
                    TransmitFileToQueueStorage();
                    fouts = $"Result=Wysłano plik {blobParams.localFileName} do kolejki {blobParams.queueName}";
                }
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
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Wysłano plik {blobParams.localFileName} do kolejki {blobParams.queueName}", "AzureBlobStorageLibrary");
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
                if (blobParams.asyncProcess)
                {
                    Task.Run(() => GetFileFromQueueStorage());
                    fouts = $"Result=Trwa pobieranie {blobParams.localFileName} z kolejki {blobParams.queueName}";
                    res = 0;
                }
                else
                {
                    res = GetFileFromQueueStorage();
                    if (res == 0)
                        fouts = $"Result=Jest lokalnie plik {blobParams.localFileName} z kolejki {blobParams.queueName}";
                }
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
            if (!queueClient.Exists())
                return BlobStorageUtility.ShowQueueWarning(1, blobParams);
            var response = queueClient.PeekMessage().Value;
            if (response == null)
                return BlobStorageUtility.ShowQueueWarning(2, blobParams);
            File.WriteAllText(blobParams.localFileName, response.Body.ToString());
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Jest lokalnie plik {blobParams.localFileName} z kolejki {blobParams.queueName}", "AzureBlobStorageLibrary");
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
                if (blobParams.asyncProcess)
                {
                    Task.Run(() => PopFileFromQueueStorage());
                    fouts = $"Result=Trwa ściąganie {blobParams.localFileName} z kolejki {blobParams.queueName}";
                    res = 0;
                }
                else
                {
                    res = PopFileFromQueueStorage();
                    if (res == 0)
                        fouts = $"Result=Ściągnięto plik {blobParams.localFileName} z kolejki {blobParams.queueName}";
                }
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
            if (!queueClient.Exists())
                return BlobStorageUtility.ShowQueueWarning(1, blobParams);
            QueueMessage response = queueClient.ReceiveMessage().Value;
            if (response == null)
                return BlobStorageUtility.ShowQueueWarning(2, blobParams);
            File.WriteAllText(blobParams.localFileName, response.Body.ToString());
            if (blobParams.deleteFile)
                queueClient.DeleteMessage(response.MessageId, response.PopReceipt);
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"ściągnięto plik {blobParams.localFileName} z kolejki {blobParams.queueName}", "AzureBlobStorageLibrary");
            return 0;
        }
    }
}
