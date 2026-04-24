namespace Student_Dental_Clinic.Models
{
    public class AcademicPerformance
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public double CumulativeGPA { get; set; }
        public int CompletedCases { get; set; }
        public int SubmittedReports { get; set; }

        public double AverageRating { get; set; }
        public int Attendance { get; set; }
    }
}
