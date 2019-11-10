using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class ProjectBindingModel : BaseViewModel
    {
        public ProjectBindingModel()
        {
            ProjectClients = new List<ProjectClientBindingModel>();
            ProjectImages = new List<ProjectImageBindingModel>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Languages { get; set; }
        public string Libraries { get; set; }
        public string Tools { get; set; }
        public DateTime StartedOn { get; set; }
        public string Status { get; set; }
        public DateTime? CompletedOn { get; set; }

        public List<ProjectClientBindingModel> ProjectClients { get; set; }
        public List<ProjectImageBindingModel> ProjectImages { get; set; }
    }

    public class ProjectClientBindingModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Organization { get; set; }
        public string Description { get; set; }
    }

    public class ProjectImageBindingModel : BaseViewModel
    {
        public IFormFile Image { get; set; }
        public bool IsPrimary { get; set; }
    }
}
