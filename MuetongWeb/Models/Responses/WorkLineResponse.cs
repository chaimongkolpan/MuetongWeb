using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class WorkLineResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public WorkLineResponse() { }
        public WorkLineResponse(Line line) 
        {
            Id = line.Id;
            Name = line.Name;
        }
    }
}
