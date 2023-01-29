using MuetongWeb.Models.Entities;

namespace MuetongWeb.Models.Responses
{
    public class ProjectCollectionResponse
    {
        public int Total { get; set; } = 0;
        public List<ProjectResponse> Projects { get; set; } = new List<ProjectResponse>();
        public ProjectCollectionResponse() { }
        public ProjectCollectionResponse(IEnumerable<Project> projects)
        {
            Total = projects.Count();
            foreach(var project in projects)
            {
                Projects.Add(new ProjectResponse(project));
            }
        }
    }
}
