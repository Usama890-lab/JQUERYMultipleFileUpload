using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jFileUpload.Models
{
    public class JsonFiles
    {
        public UploadFilesResult[] files;
        public string TempFolder { get; set; }
        public JsonFiles(List<UploadFilesResult> filesList)
        {
            files = new UploadFilesResult[filesList.Count];
            for (int i = 0; i < filesList.Count; i++)
            {
                files[i] = filesList.ElementAt(i);
            }
        }
    }
}
