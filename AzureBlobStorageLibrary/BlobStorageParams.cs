using System;

namespace AzureBlobStorageLibrary
{
    public class BlobStorageParams
    {
        public string connectionString, containerName, localFileName;
        public bool deleteFile;

        public static string GetTextParameter(string text, string parName)
        {
            foreach (string line in text.Split(new char[] { '\n', '\r' }))
                if (line.IndexOf(parName + "=", StringComparison.OrdinalIgnoreCase) == 0)
                    return line.Substring(parName.Length + 1);
            return "";
        }
        public static bool GetBoolParameter(string text, string parName)
        {
            var cRes = GetTextParameter(text, parName).Trim();
            return (cRes == "T") || (cRes == "t");
        }
        public BlobStorageParams(string fpars)
        {
            connectionString = GetTextParameter(fpars, "connectionString");
            containerName = GetTextParameter(fpars, "containerName");
            localFileName = GetTextParameter(fpars, "localFileName");
            deleteFile = GetBoolParameter(fpars, "deleteFile");
        }
    }
}