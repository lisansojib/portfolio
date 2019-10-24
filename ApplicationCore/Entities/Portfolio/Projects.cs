using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities.Portfolio
{
    public class Projects : BaseEntity
    {
        public Projects()
        {
            ProjectClients = new HashSet<ProjectClients>();
            ProjectImages = new HashSet<ProjectImages>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Libraries { get; set; }
        public string Tools { get; set; }
        public DateTime AddedOn { get; set; }
        public int AddedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? UpdatedBy { get; set; }

        public virtual ICollection<ProjectClients> ProjectClients { get; set; }
        public virtual ICollection<ProjectImages> ProjectImages { get; set; }
    }
}
