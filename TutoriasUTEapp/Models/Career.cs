using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutoriasUTEapp.Models
{
    public class Career
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(100, ErrorMessage = "La longitud es de maximo 100 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo Abbreviation es obligatorio")]
        [StringLength(10, ErrorMessage = "La longitud es de maximo 10 caracteres")]
        public string Abbreviation { get; set; }


        public virtual ICollection<ClassGroup> ClassGroups { get; set; }

    }
}