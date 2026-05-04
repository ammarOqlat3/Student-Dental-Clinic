namespace Student_Dental_Clinic.Models
{
    public class Report
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CaseId { get; set; }
        public Case Case { get; set; }

        // محتوى التقرير العام
        public string Content { get; set; }

        // الخانات التفصيلية للتقرير
        public string? ChiefComplaint { get; set; }
        public string? DiagnosisNotes { get; set; }
        public string? TreatmentDone { get; set; }
        public string? FollowUpPlan { get; set; }
        public string? Complications { get; set; }

        // صور قبل وبعد
        public string? BeforeImagePath { get; set; }
        public string? AfterImagePath { get; set; }

        // تاريخ الرفع
        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        // تقييم الدكتور
        public int? Rating { get; set; }
        public int? MaxGrade { get; set; }   // الدرجة القصوى (10 / 20 / 100)
        public string? DoctorFeedback { get; set; }
    }
}