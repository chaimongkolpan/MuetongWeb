using MuetongWeb.Constants;
using MuetongWeb.Helpers;

namespace MuetongWeb.Models.Responses
{
    public class FileResponse
    {
        public string caption { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
        public long key { get; set; }
        public FileExtraResponse? extra { get; set; }
        public FileResponse() { }
        public FileResponse(Models.Entities.File file)
        {
            caption = FileHelpers.GetFilename(file.Path);
            url = FileConstants.DeletePath + file.Id;
            key = file.Id;
            extra = new FileExtraResponse() { id = file.Id };
        }
    }
    public class FileExtraResponse
    {
        public long id { get; set; }
    }
}
