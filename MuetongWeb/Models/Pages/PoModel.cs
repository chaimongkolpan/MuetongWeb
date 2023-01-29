using System;
using System.Collections.Generic;
using MuetongWeb.Models.Entities;
using MuetongWeb.Models.Responses;

namespace MuetongWeb.Models.Pages
{
    public class PoModel : PageModel
    {
        public bool CanEdit { get; set; } = false;
        public List<ProjectResponse> Projects { get; set; } = new List<ProjectResponse>();
        public PoModel() { }
        public PoModel(List<ProjectResponse>? projects, bool editPermit = false)
        {
            if (projects != null)
                Projects = projects;
            CanEdit = editPermit;
        }
    }
}

