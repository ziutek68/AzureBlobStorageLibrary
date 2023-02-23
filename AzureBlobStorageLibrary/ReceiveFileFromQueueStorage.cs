using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class ReceiveFileFromQueueStorage
    {
        private BlobStorageParams blobParams;
        private WaitForm waitForm;
        public int Execute(string _1, string fpars, ref string fouts)
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
            MessageOnBegin();
            try
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
                return 0;
            }
            finally
            {
                MessageOnFinish();
            }
        }
        private void MessageOnBegin()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm = new WaitForm($"Trwa ściąganie { blobParams.localFileName} z kolejki { blobParams.queueName}");
        }
        private void MessageOnFinish()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"ściągnięto plik {blobParams.localFileName} z kolejki {blobParams.queueName}", "AzureBlobStorageLibrary");
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm.Close();
        }
    }
}
