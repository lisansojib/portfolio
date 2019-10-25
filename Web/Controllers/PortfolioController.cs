using ApplicationCore.Entities.Portfolio;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PortfolioController : ApiControllerBase
    {
        private readonly IEfRepository<Projects> _projectRepository;

        public PortfolioController(IEfRepository<Projects> projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IActionResult Get()
        {
            var response = new string[] { "Hello", "World" };
            return Ok(response);
        }
    }
}