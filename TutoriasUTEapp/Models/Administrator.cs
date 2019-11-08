using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TutoriasUTEapp.Models
{
    public class Administrator
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo UserName es obligatorio")]
        [StringLength(25, ErrorMessage = "La longitud es de maximo 25 caracteres")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo UserPassword es obligatorio")]
        [StringLength(25, ErrorMessage = "La longitud es de maximo 25 caracteres")]
        public string UserPassword { get; set; }

    }
}