namespace Student_Dental_Clinic.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string Semester { get; set; }

        public string Location { get; set; }
        public string Schedule { get; set; }

        // الدكتور المسؤول
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        // الطلاب
        public List<StudentCourse> StudentCourses { get; set; }
    }
}
