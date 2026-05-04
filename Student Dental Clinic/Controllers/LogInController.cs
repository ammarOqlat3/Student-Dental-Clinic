using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Dental_Clinic.Data;
using Student_Dental_Clinic.Models;

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
            if (userId == null) return RedirectToAction("Login", "LogIn");

            var patients = _context.Patients
                                   .Where(p => p.UserId == userId)
                                   .ToList();
            return View(patients);
        }

        public IActionResult Students()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

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

            if (student == null) return NotFound();

            var reports = _context.Reports
                .Where(r => r.StudentId == student.Id)
                .Include(r => r.Case)
                    .ThenInclude(c => c.Patient)
                .OrderByDescending(r => r.SubmittedAt)
                .ToList();

            var caseRequests = _context.CaseRequests
                .Where(r => r.StudentId == student.Id)
                .OrderByDescending(r => r.RequestDate)
                .ToList();

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
                .Include(p => p.Student)
                .ToList();

            var doctor = _context.Doctors
                .Include(d => d.User)
                .FirstOrDefault(d => d.UserId == userId);

            var reports = _context.Reports
                .Include(r => r.Student)
                .Include(r => r.Case)
                    .ThenInclude(c => c.Patient)
                .OrderByDescending(r => r.SubmittedAt)
                .ToList();

            var allStudents = _context.Students
                .Include(s => s.Cases)
                    .ThenInclude(c => c.Patient)
                .ToList();

            ViewBag.Doctor = doctor;
            ViewBag.Students = allStudents;
            ViewBag.PatientsCount = patients.Count;
            ViewBag.StudentsCount = allStudents.Count;
            ViewBag.Reports = reports;
            ViewBag.ReportsCount = reports.Count;
            ViewBag.PendingReportsCount = reports.Count(r => !r.Rating.HasValue);
            return View(patients);
        }

        [HttpPost]
        public IActionResult RateReport(int reportId, int rating, string? feedback, int maxGrade = 20)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            var report = _context.Reports.FirstOrDefault(r => r.Id == reportId);
            if (report == null) return Json(new { success = false, message = "Report not found" });

            report.Rating = rating;
            report.MaxGrade = maxGrade;
            report.DoctorFeedback = feedback ?? "";
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Create(Patient patient, string hasAllergy, IFormFile ToothImage)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            patient.UserId = userId.Value;
            patient.HasAllergy = hasAllergy == "yes";

            if (ToothImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ToothImage.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                    ToothImage.CopyTo(stream);
                patient.ToothImagePath = fileName;
            }

            _context.Patients.Add(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

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
                    ? parsedDate : DateTime.Now;

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
            return RedirectToAction("Doctor");
        }

        // ✅ تحديث حالة الكيس إلى Completed
        [HttpPost]
        public IActionResult MarkCaseComplete(int caseId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            var student = _context.Students.FirstOrDefault(s => s.UserId == userId);
            if (student == null) return NotFound();

            var theCase = _context.Cases
                .FirstOrDefault(c => c.Id == caseId && c.StudentId == student.Id);

            if (theCase != null)
            {
                theCase.Status = "Completed";
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Case not found" });
        }

        // ✅ رفع تقرير جديد
        [HttpPost]
        public async Task<IActionResult> SubmitReport(
            int CaseId,
            string Content,
            string? ChiefComplaint,
            string? DiagnosisNotes,
            string? TreatmentDone,
            string? FollowUpPlan,
            string? Complications,
            IFormFile? BeforeImage,
            IFormFile? AfterImage)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            var student = _context.Students.FirstOrDefault(s => s.UserId == userId);
            if (student == null) return NotFound();

            // حذف التقرير القديم لنفس الكيس إذا موجود
            var existingReport = _context.Reports.FirstOrDefault(r => r.CaseId == CaseId && r.StudentId == student.Id);
            if (existingReport != null)
            {
                // احذف الصور القديمة إذا موجودة
                DeleteReportImages(existingReport);
                _context.Reports.Remove(existingReport);
            }

            var report = new Report
            {
                StudentId = student.Id,
                CaseId = CaseId,
                Content = Content ?? "",
                ChiefComplaint = ChiefComplaint,
                DiagnosisNotes = DiagnosisNotes,
                TreatmentDone = TreatmentDone,
                FollowUpPlan = FollowUpPlan,
                Complications = Complications,
                SubmittedAt = DateTime.Now
            };

            // رفع صورة قبل
            if (BeforeImage != null && BeforeImage.Length > 0)
            {
                string fileName = "before_" + Guid.NewGuid() + Path.GetExtension(BeforeImage.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await BeforeImage.CopyToAsync(stream);
                report.BeforeImagePath = fileName;
            }

            // رفع صورة بعد
            if (AfterImage != null && AfterImage.Length > 0)
            {
                string fileName = "after_" + Guid.NewGuid() + Path.GetExtension(AfterImage.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await AfterImage.CopyToAsync(stream);
                report.AfterImagePath = fileName;
            }

            _context.Reports.Add(report);
            _context.SaveChanges();

            return Json(new { success = true });
        }

        // ✅ حذف تقرير
        [HttpPost]
        public IActionResult DeleteReport(int reportId)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return Unauthorized();

            var student = _context.Students.FirstOrDefault(s => s.UserId == userId);
            if (student == null) return NotFound();

            var report = _context.Reports.FirstOrDefault(r => r.Id == reportId && r.StudentId == student.Id);
            if (report != null)
            {
                DeleteReportImages(report);
                _context.Reports.Remove(report);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        private void DeleteReportImages(Report report)
        {
            var wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!string.IsNullOrEmpty(report.BeforeImagePath))
            {
                var path = Path.Combine(wwwroot, report.BeforeImagePath);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }
            if (!string.IsNullOrEmpty(report.AfterImagePath))
            {
                var path = Path.Combine(wwwroot, report.AfterImagePath);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }
        }
    }
}