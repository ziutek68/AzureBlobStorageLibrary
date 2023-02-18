using System;

namespace AzureBlobStorageLibrary
{
    public class BlobStorageParams
    {
        public string connectionString, containerName, localFileName, queueName;
        public bool deleteFile;

        public static string GetTextParameter(string text, string parName)
        {
            foreach (string line in text.Split(new char[] { '\n', '\r' }))
                if (line.IndexOf(parName + "=", StringComparison.OrdinalIgnoreCase) == 0)
                    return line.Substring(parName.Length + 1);
            return "";
        }
        public static bool GetBoolParameter(string text, string parName, bool defVal)
        {
            var cRes = GetTextParameter(text, parName).Trim();
            if (String.IsNullOrEmpty(cRes)) return defVal;
            return (cRes == "T") || (cRes == "t");
        }
        public BlobStorageParams(string fpars)
        {
            connectionString = GetTextParameter(fpars, "connectionString");
            containerName = GetTextParameter(fpars, "containerName");
            queueName = GetTextParameter(fpars, "queueName");
            localFileName = GetTextParameter(fpars, "localFileName");
            deleteFile = GetBoolParameter(fpars, "deleteFile", String.IsNullOrEmpty(containerName));
        }
    }
}