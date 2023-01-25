using MuetongWeb.Models.Responses;

namespace MuetongWeb.Models.Pages
{
    public class BillingModel : PageModel
    {
        public bool CanEdit { get; set; } = false;
        public List<ProjectResponse> Projects { get; set; } = new List<ProjectResponse>();
        public BillingModel() { }
        public BillingModel(List<ProjectResponse>? projects, bool editPermit = false)
        {
            if (projects != null)
                Projects = projects;
            CanEdit = editPermit;
        }
    }
}
