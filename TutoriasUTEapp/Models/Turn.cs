using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class Turn
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(30, ErrorMessage = "La longitud es de maximo 30 caracteres")]
        public string Description { get; set; }

        public virtual ICollection<ClassGroup> ClassGroups { get; set; }
    }
}