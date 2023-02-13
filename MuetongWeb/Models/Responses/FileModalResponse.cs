using MuetongWeb.Helpers;

namespace MuetongWeb.Models.Responses
{
    public class FileModalResponse
    {
        public List<FileResponse> Files { get; set; } = new List<FileResponse>();
        public List<string> FilePreviews { get; set; } = new List<string>();
        public FileModalResponse() { }
        public FileModalResponse(List<Models.Entities.File> files)
        {
            Files.AddRange(files.Select(file => new FileResponse(file)).ToList());
            FilePreviews.AddRange(files.Select(file => FileHelpers.GetUrlTag(file.Id, file.Extention, file.Path)).ToList());
        }
    }
}
