using Microsoft.AspNetCore.Mvc;

namespace Web.UI.Controllers
{
    public class ResumeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}