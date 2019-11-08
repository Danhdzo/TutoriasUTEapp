using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Registration es obligatorio")]
        [StringLength(15, ErrorMessage = "La longitud es de 15 caracteres")]
        public string Registration { get; set; }

        //cambiado
        [Required(ErrorMessage = "El campo FirstMidName es obligatorio")]
        [StringLength(35, ErrorMessage = "La longitud es de 30 caracteres")]
        public string FirstMidName { get; set; }

        //cambiado
        [Required(ErrorMessage = "El campo LastNameP es obligatorio")]
        [StringLength(30, ErrorMessage = "La longitud es de 25 caracteres")]
        public string LastNameP { get; set; }

        //cambiado
        [Required(ErrorMessage = "El campo LastNameM es obligatorio")]
        [StringLength(30, ErrorMessage = "La longitud es de 25 caracteres")]
        public string LastNameM { get; set; }

        [StringLength(20, ErrorMessage = "La longitud es de 20 caracteres")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "El campo EmergencyTelephone es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud es de 20 caracteres")]
        public string EmergencyTelephone { get; set; }

        //cambiado
        [Required(ErrorMessage = "El campo AcademicEmail es obligatorio")]
        [StringLength(45, ErrorMessage = "La longitud es de 40 caracteres")]
        public string AcademicEmail { get; set; }

        [Required(ErrorMessage = "El campo Birthday es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud es de 20 caracteres")]
        public string Birthday { get; set; }

        [ForeignKey("Situation")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Situation debe ser un numero entero")]
        public int SituationID { get; set; }
        public Situation Situation { get; set; }

        public virtual ICollection<ClassGroupStudent> ClassGroupStudents { get; set; }
        public virtual ICollection<StudentCourseComment> StudentCourseComments { get; set; }
        public virtual ICollection<StudentCourseGrade> StudentCourseGrades { get; set; }
    }
}