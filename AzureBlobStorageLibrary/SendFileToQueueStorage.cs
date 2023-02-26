using Azure.Storage.Queues;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class SendFileToQueueStorage
    {
        private BlobStorageParams blobParams;
        private WaitForm waitForm;
        public int Execute(string _1, string fpars, ref string fouts)
        {
            try
            {
                blobParams = new BlobStorageParams(fpars);
                if (blobParams.asyncProcess)
                {
                    Task.Run(() => TransmitFileToQueueStorage());
                    fouts = $"Result=Trwa wysyłanie {blobParams.localFileName} do kolejki {blobParams.queueName}";
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
            MessageOnBegin();
            try
            {
                QueueClient queueClient = new QueueClient(blobParams.connectionString, blobParams.queueName);
                var content = File.ReadAllText(blobParams.localFileName);
                queueClient.SendMessage(content);
                if (blobParams.deleteFile) File.Delete(blobParams.localFileName);
            }
            finally
            {
                MessageOnFinish();
            }
        }
        private void MessageOnBegin()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm = new WaitForm($"Trwa wysyłanie { blobParams.localFileName} do kolejki { blobParams.queueName}");
        }
        private void MessageOnFinish()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Wysłano plik {blobParams.localFileName} do kolejki {blobParams.queueName}", "AzureBlobStorageLibrary");
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm.Close();
        }
    }
}
