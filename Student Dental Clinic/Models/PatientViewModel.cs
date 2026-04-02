namespace Student_Dental_Clinic.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string CaseDescription { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; } // مثال: متاحة، قيد الانتظار
    }
}
