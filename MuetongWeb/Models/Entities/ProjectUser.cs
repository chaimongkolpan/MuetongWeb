using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class ProjectUser
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public long UserId { get; set; }

        public virtual Project Project { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
