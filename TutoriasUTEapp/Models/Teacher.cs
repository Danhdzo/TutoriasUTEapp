using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class Teacher
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo EmployeeID es obligatorio")]
        [StringLength(8, ErrorMessage = "La longitud es de 8 caracteres")]
        public string EmployeeID { get; set; }

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

        //cambiado
        [Required(ErrorMessage = "El campo UserName es obligatorio")]
        [StringLength(30, ErrorMessage = "La longitud es de maximo 25 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo UserPassword es obligatorio")]
        [StringLength(25, ErrorMessage = "La longitud es de maximo 25 caracteres")]
        public string UserPassword { get; set; }


        public virtual ICollection<TeacherRole> TeacherRoles { get; set; }
        public virtual ICollection<ClassGroup> ClassGroups { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

    }
}