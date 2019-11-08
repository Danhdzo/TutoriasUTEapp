using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class StudentCourseComment
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public Student Student { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }

        [Key, Column(Order = 2)]
        [Required(ErrorMessage = "El campo Date es obligatorio")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(255, ErrorMessage = "La longitud es de maximo 255 caracteres")]
        public string Description { get; set; }
    }
}