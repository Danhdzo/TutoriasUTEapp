using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TutoriasUTEapp.Models
{
    public class Log
    {

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "El campo Date es obligatorio")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "El campo Description es obligatorio")]
        [StringLength(255, ErrorMessage = "La longitud es de maximo 255 caracteres")]
        public string Description { get; set; }


    }
}