namespace Student_Dental_Clinic.Models
{
    public class Report
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CaseId { get; set; }
        public Case Case { get; set; }

        public string Content { get; set; }

        public int? Rating { get; set; } // من الدكتور
        public string DoctorFeedback { get; set; }
    }
}
