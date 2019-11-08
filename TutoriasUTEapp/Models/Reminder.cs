using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class Reminder
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Date es obligatorio")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(50, ErrorMessage = "La longitud es de maximo 50 caracteres")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(255, ErrorMessage = "La longitud es de maximo 255 caracteres")]
        public string Description { get; set; }

        [ForeignKey("Role")]
        [Required(ErrorMessage = "El campo Role es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Role debe ser un numero entero")]
        public int RoleID { get; set; }
        public Role Role { get; set; }

    }
}