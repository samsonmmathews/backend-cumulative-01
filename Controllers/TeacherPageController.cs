using Microsoft.AspNetCore.Mvc;

namespace Backend_Cumulative_01.Controllers
{
    public class TeacherPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
