namespace Student_Dental_Clinic.Models
{
    public class Patient
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public bool HasAllergy { get; set; }

        public string? AllergyDetails { get; set; }

        public string TreatmentType { get; set; }

        // لحفظ اسم الصورة فقط
        public string? ToothImagePath { get; set; }
        public string Status { get; set; } = "Pending"; // الحالة
        public string? DoctorName { get; set; } = null; // الدكتور (مبدئياً فاضي)

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; }
        public int? StudentId { get; set; }
        public List<Case> Cases { get; set; }

        public Student? Student { get; set; }
    }
}