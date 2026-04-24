namespace Student_Dental_Clinic.Models
{
    public class CaseRequest
    {
        public int Id { get; set; }

        // الطالب اللي قدم الطلب
        public int StudentId { get; set; }
        public Student Student { get; set; }

        // معلومات الشخص (المريض)
        public string PatientName { get; set; }
        public string PhoneNumber { get; set; }

        // صلة القرابة (أب، صديق، ...)
        public string Relationship { get; set; }

        // نوع العلاج
        public string TreatmentType { get; set; }

        // سبب الطلب
        public string Description { get; set; }

        // حالة الطلب
        public string Status { get; set; } // Pending / Approved / Rejected

        // الدكتور اللي رح يشوف الطلب
        public int? DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        // تاريخ الطلب
        public DateTime RequestDate { get; set; } = DateTime.Now;
    }
}
