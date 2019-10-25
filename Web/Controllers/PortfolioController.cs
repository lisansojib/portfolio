using ApplicationCore.Entities.Portfolio;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PortfolioController : ApiControllerBase
    {
        private readonly IEfRepository<Projects> _projectRepository;
        private readonly IMapper _mapper;

        public PortfolioController(IEfRepository<Projects> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = new string[] { "Hello", "World" };
            return Ok(response);
        }
    }
}