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
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var student = _context.Students
                .Include(s => s.User)
                .Include(s => s.Performance)
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                .Include(s => s.Cases)
                    .ThenInclude(c => c.Patient)
                .Include(s => s.Cases)
                    .ThenInclude(c => c.Doctor)
                .FirstOrDefault(s => s.UserId == userId);

            if (student == null)
            {
                return NotFound();
            }

            var reports = _context.Reports
                .Where(r => r.StudentId == student.Id)
                .Include(r => r.Case)
                    .ThenInclude(c => c.Patient)
                .OrderByDescending(r => r.Id)
                .ToList();

            var caseRequests = _context.CaseRequests
                .Where(r => r.StudentId == student.Id)
                .OrderByDescending(r => r.RequestDate)
                .ToList();

            // جلب كل المرضى المرتبطين بالطالب هاض مع كل حالاتهم
            var myPatients = _context.Patients
                .Where(p => p.StudentId == student.Id)
                .Include(p => p.Cases)
                    .ThenInclude(c => c.Doctor)
                .ToList();

            ViewBag.Reports = reports;
            ViewBag.CaseRequests = caseRequests;
            ViewBag.MyPatients = myPatients;

            return View(student);
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
        public IActionResult AssignStudent(int PatientId, int StudentId, string SessionDate, string SessionTime, string ChairNumber)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == PatientId);

            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var doctor = _context.Doctors.FirstOrDefault(d => d.UserId == userId);

            if (patient != null && doctor != null)
            {
                patient.StudentId = StudentId;
                patient.Status = "Assigned";

                DateTime sessionDate = DateTime.TryParse(SessionDate, out DateTime parsedDate)
                    ? parsedDate
                    : DateTime.Now;

                var newCase = new Case
                {
                    PatientId = PatientId,
                    StudentId = StudentId,
                    DoctorId = doctor.Id,
                    Date = sessionDate,
                    Time = string.IsNullOrEmpty(SessionTime) ? DateTime.Now.ToString("HH:mm") : SessionTime,
                    Status = "Pending",
                    TreatmentType = patient.TreatmentType ?? "General",
                    Location = string.IsNullOrEmpty(ChairNumber) ? "TBD" : $"Chair {ChairNumber}"
                };

                _context.Cases.Add(newCase);
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

