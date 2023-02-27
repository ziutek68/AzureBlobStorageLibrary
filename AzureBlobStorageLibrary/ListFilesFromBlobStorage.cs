using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureBlobStorageLibrary
{
    [ComVisible(true)]
    public class ListFilesFromBlobStorage
    {
        private BlobStorageParams blobParams;
        private string outputs;
        private WaitForm waitForm;
        private void GetListFromBlobStorage()
        {
            MessageOnBegin();
            try
            {
                var blobList = BlobStorageUtility.GetBlobs(blobParams);
                int lp = 0;
                StringBuilder result = new StringBuilder();
                foreach (var blobItem in blobList)
                    result.AppendFormat("File{0}={1}\n", ++lp, blobItem.Name);
                outputs = result.ToString();
            }
            finally
            {
                MessageOnFinish();
            }
        }
        private void MessageOnBegin()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm = new WaitForm($"Trwa pobieranie wykazu plików z kontenera {blobParams.containerName}");
        }
        private void MessageOnFinish()
        {
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtOnEnd)
                MessageBox.Show($"Lista plików w kontenerze {blobParams.containerName}:\n\n" + outputs, "AzureBlobStorageLibrary");
            if (blobParams.messsageType == BlobStorageParams.MsgType.mtAllTime)
                waitForm.Close();
        }
        public int Execute(string _1, string fpars, ref string fouts)
        {
            try
            {
                blobParams = new BlobStorageParams(fpars);
                if (blobParams.asyncProcess)
                {
                    Task.Run(() => GetListFromBlobStorage());
                    fouts = $"Result=Trwa pobieranie wykazu plików z kontenera {blobParams.containerName}";
                }
                else
                {
                    GetListFromBlobStorage();
                    fouts = outputs;
                }
                return 0;
            }
            catch (System.Exception ex)
            {
                fouts = "ErrorMessage=" + ex.Message;
            }
            return -1;
        }
    }
}