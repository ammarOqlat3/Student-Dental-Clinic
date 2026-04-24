namespace Student_Dental_Clinic.Models
{
    public class Case
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int? StudentId { get; set; } // ممكن ما يكون متعين
        public Student Student { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime Date { get; set; }
        public string Time { get; set; }

        public string Status { get; set; } // Pending / Approved / Done
    }
}
