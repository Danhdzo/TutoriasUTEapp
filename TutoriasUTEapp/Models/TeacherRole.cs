using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class TeacherRole
    {

        [Key, Column(Order = 0)]
        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public Role Role { get; set; }

    }
}