using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities.Portfolio
{
    public class Project : BaseEntity
    {
        public Project()
        {
            ProjectClients = new HashSet<ProjectClient>();
            ProjectImages = new HashSet<ProjectImage>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Libraries { get; set; }
        public string Tools { get; set; }
        public DateTime AddedOn { get; set; }
        public int AddedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? UpdatedBy { get; set; }

        public virtual ICollection<ProjectClient> ProjectClients { get; set; }
        public virtual ICollection<ProjectImage> ProjectImages { get; set; }
    }
}
