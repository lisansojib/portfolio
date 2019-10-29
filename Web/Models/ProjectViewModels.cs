using System.Collections.Generic;

namespace Web.Models
{
    public class ProjectViewModel : BaseViewModel
    {
        public ProjectViewModel()
        {
            ProjectClients = new List<ProjectClientVeiwModel>();
            ProjectImages = new List<ProjectImageViewModel>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Libraries { get; set; }
        public string Tools { get; set; }

        public List<ProjectClientVeiwModel> ProjectClients { get; set; }
        public List<ProjectImageViewModel> ProjectImages { get; set; }
    }

    public class ProjectClientVeiwModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Organization { get; set; }
        public string Description { get; set; }
    }

    public class ProjectImageViewModel : BaseViewModel
    {
        public string Caption { get; set; }
        public string Path { get; set; }
        public bool IsPrimary { get; set; }
    }
}
