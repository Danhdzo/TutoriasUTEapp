using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace TutoriasUTEapp.Models
{
    public class TutoriasUTEDbContext : DbContext
    {

        public TutoriasUTEDbContext() : base("TutoriasUTEDbContext")
        {
            //Constructor vacio
            //La cadena de conexion se llamara...
        }

        public DbSet<Log> Logs { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Turn> Turns { get; set; }
        public DbSet<Modality> Modalities { get; set; }
        public DbSet<Situation> Situations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<TeacherRole> TeacherRoles { get; set; }
        public DbSet<ClassGroup> ClassGroups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<ClassGroupCourse> ClassGroupCourses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ClassGroupStudent> ClassGroupStudents { get; set; }
        public DbSet<StudentCourseComment> StudentCourseComments { get; set; }
        public DbSet<StudentCourseGrade> StudentCourseGrades { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}