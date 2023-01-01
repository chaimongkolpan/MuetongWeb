using System;
using System.Collections.Generic;

namespace MuetongWeb.Models.Entities
{
    public partial class File
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public string Type { get; set; } = null!;
        public string Path { get; set; } = null!;
        public string Extention { get; set; } = null!;
    }
}
