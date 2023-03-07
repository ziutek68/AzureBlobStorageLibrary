using Azure.Messaging.ServiceBus;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class SendFileToQueueServiceBus
    {
        private BlobStorageParams blobParams;
        private WaitForm waitForm;
        public int Execute(string _1, string fpars, ref string fouts)
        {
            try
            {
                blobParams = new BlobStorageParams(fpars);
                PutFileToQueueServiceBusAsync().GetAwaiter();
                fouts = $"Result=Trwa wysyłanie {blobParams.localFileName} do kolejki {blobParams.queueName} w S.B.";
                return 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return -1;
        }
        private async Task PutFileToQueueServiceBusAsync()
        {
            MessageOnBegin();
            try
            {
                var client = new ServiceBusClient(blobParams.connectionString);
                ServiceBusSender sender = client.CreateSender(blobParams.queueName);
                try
                {
                    var content = File.ReadAllText(blobParams.localFileName);
                    var message = new ServiceBusMessage(content);
                    await sender.SendMessageAsync(message);
                    if (blobParams.deleteFile) File.Delete(blobParams.localFileName);
                }
                finally
                {
                    await sender.DisposeAsync();
                    await client.DisposeAsync();
                }
            }
            catch (System.Exception ex)
            {
                if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                    blobParams.messsageType = BlobStorageParams.MsgType.mtNone;
                MessageBox.Show(ex.ToString(), "AzureBlobStorageLibrary", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                MessageOnFinish();
            }
        }
        private void MessageOnBegin()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm = new WaitForm($"Trwa wysyłanie {blobParams.localFileName} do kolejki {blobParams.queueName} w S.B.");
        }
        private void MessageOnFinish()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Wysłano plik {blobParams.localFileName} do kolejki {blobParams.queueName} w S.B.", "AzureBlobStorageLibrary");
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm.Close();
        }
    }
}
