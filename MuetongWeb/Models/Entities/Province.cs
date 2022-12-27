using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class Province
    {
        public Province()
        {
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public string NameTh { get; set; } = null!;
        public string NameEn { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
