using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Dental_Clinic.Data;
using Student_Dental_Clinic.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace Student_Dental_Clinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

      

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult login()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                user.Role = "Patient";

                string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&]).{8,}$";

                if (!Regex.IsMatch(user.Password, pattern))
                {
                    TempData["Error"] = "Password must be at least 8 characters and include letters, numbers, and a special character.";
                    return RedirectToAction("login");
                }

                var existUser = _context.Users.FirstOrDefault(x => x.Username == user.Username);
                if (existUser != null)
                {
                    TempData["Error"] = "Username already exists!";
                    return RedirectToAction("login");
                }

                var existEmail = _context.Users.FirstOrDefault(x => x.Email == user.Email);
                if (existEmail != null)
                {
                    TempData["Error"] = "Email already exists!";
                    return RedirectToAction("login");
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["Success"] = "Account created successfully!";
                return RedirectToAction("login");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("login");
            }
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
            {
                TempData["Error"] = "Wrong username or password!";
                return RedirectToAction("login");
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Role", user.Role);

            // 🔥 Doctor
            if (user.Role == "Doctor")
            {
                var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == user.Id);

                if (doctor == null)
                {
                    TempData["Error"] = "Doctor profile not found!";
                    return RedirectToAction("login");
                }

                return RedirectToAction("doctor", "LogIn");
            }

            // 🔥 Student
            if (user.Role == "Student")
            {
                var student = _context.Students.FirstOrDefault(s => s.UserId == user.Id);

                if (student == null)
                {
                    TempData["Error"] = "Student profile not found!";
                    return RedirectToAction("login");
                }

                return RedirectToAction("Students", "LogIn");
            }

            // Patient
            return RedirectToAction("Index", "LogIn");
        }
    }
}