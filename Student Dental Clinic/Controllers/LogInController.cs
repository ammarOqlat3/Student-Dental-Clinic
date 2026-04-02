using Microsoft.AspNetCore.Mvc;

namespace Student_Dental_Clinic.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Students()
        {
            return View();
        }
    }
}
