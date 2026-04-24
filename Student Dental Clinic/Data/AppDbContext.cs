using Microsoft.EntityFrameworkCore;
using Student_Dental_Clinic.Models;

namespace Student_Dental_Clinic.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        // User
        public DbSet<User> Users { get; set; }

        // Doctor & Student
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Student> Students { get; set; }

        // Courses
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        // Patients & Cases
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Case> Cases { get; set; }

        // Reports
        public DbSet<Report> Reports { get; set; }

        // Academic الأداء
        public DbSet<AcademicPerformance> AcademicPerformances { get; set; }

        // طلبات الحالات
        public DbSet<CaseRequest> CaseRequests { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Case>()
      .HasOne(c => c.Patient)
      .WithMany(p => p.Cases)
      .HasForeignKey(c => c.PatientId)
      .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Case>()
                .HasOne(c => c.Doctor)
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Case>()
                .HasOne(c => c.Student)
                .WithMany()
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);
            ;

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId)
                 .OnDelete(DeleteBehavior.NoAction);
        }
    }

}