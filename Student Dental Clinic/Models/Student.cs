namespace Student_Dental_Clinic.Models
{
    public class Student
    {
        public int Id { get; set; }

        public int UserId { get; set; } // FK
        public User User { get; set; }

        public string FullName { get; set; }
        public string UniversityId { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string AcademicYear { get; set; }
        public string Semester { get; set; }

        // علاقات
        public List<StudentCourse> StudentCourses { get; set; }
        public AcademicPerformance Performance { get; set; }
    }
}
