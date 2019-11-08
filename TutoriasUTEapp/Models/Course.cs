using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class Course
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(255, ErrorMessage = "La longitud es de maximo 255 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo Units es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Units debe ser un numero entero")]
        public int Units { get; set; }

        [ForeignKey("Teacher")]
        [Required(ErrorMessage = "El campo Teacher es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Teacher debe ser un numero entero")]
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public virtual ICollection<ClassGroupCourse> ClassGroupCourses { get; set; }
        public virtual ICollection<StudentCourseComment> StudentCourseComments { get; set; }
        public virtual ICollection<StudentCourseGrade> StudentCourseGrades { get; set; }

    }
}