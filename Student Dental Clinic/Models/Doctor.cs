namespace Student_Dental_Clinic.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public int UserId { get; set; } // FK
        public User User { get; set; }

        public string FullName { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }

        public string Department { get; set; }
        public string AcademicRank { get; set; }

        public string Phone { get; set; }

        // علاقة مع الكورسات
        public List<Course> Courses { get; set; }
    }
}
