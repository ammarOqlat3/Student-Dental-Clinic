using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Dental_Clinic.Data;
using Student_Dental_Clinic.Models;
using System.Security.Claims;


namespace Student_Dental_Clinic.Controllers
{
    public class LogInController : Controller

    {
        private readonly AppDbContext _context;
        public LogInController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "LogIn");
            }

            var patients = _context.Patients
                                   .Where(p => p.UserId == userId)
                                   .ToList();

            return View(patients);
        }

        public IActionResult Students()
        {
            return View();
        }
        public IActionResult Doctor()
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;

            var patients = _context.Patients
                .Include(p => p.Student) // 🔥 مهم
                .ToList();

            var doctor = _context.Doctors
            .Include(d => d.User)
            .FirstOrDefault(d => d.UserId == userId);

            var students = _context.Users
                .Where(u => u.Role == "Student")
                .ToList();


            ViewBag.Doctor = doctor;
            ViewBag.Students = _context.Students.ToList();
            ViewBag.PatientsCount = _context.Patients.Count();   // عدد المرضى
            ViewBag.StudentsCount = _context.Students.Count();   // عدد الطلاب
            return View(patients); // 🔥 نبعث المرضى مباشرة
        }

        [HttpPost]

        public IActionResult Create(Patient patient, string hasAllergy, IFormFile ToothImage)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            patient.UserId = userId.Value;

            // تحويل yes/no → true/false
            patient.HasAllergy = hasAllergy == "yes";

            // رفع الصورة
            if (ToothImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ToothImage.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ToothImage.CopyTo(stream);
                }

                patient.ToothImagePath = fileName;
            }

            // حفظ في الداتا بيس
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        //لجلب بيانات الكريض


        [HttpPost]
        public IActionResult AssignStudent(int PatientId, int StudentId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == PatientId);

            if (patient != null)
            {
                patient.StudentId = StudentId;
                patient.Status = "Assigned";

                _context.SaveChanges();
            }

            return RedirectToAction("Doctor");
        }
        [HttpPost]
        public IActionResult UpdateProfile(Doctor doctor)
        {
            var existingDoctor = _context.Doctors.Find(doctor.Id);

            if (existingDoctor != null)
            {
                existingDoctor.FullName = doctor.FullName;
                existingDoctor.Email = doctor.Email;
                existingDoctor.Phone = doctor.Phone;
                existingDoctor.Department = doctor.Department;
                existingDoctor.AcademicRank = doctor.AcademicRank;

                _context.SaveChanges();
            }

            return RedirectToAction("Doctor"); // أو نفس الصفحة
        }



    }
}

