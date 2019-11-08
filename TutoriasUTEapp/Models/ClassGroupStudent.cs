using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class ClassGroupStudent
    {

        [Key, Column(Order = 0)]
        [ForeignKey("ClassGroup")]
        public int ClassGroupID { get; set; }
        public ClassGroup ClassGroup { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public Student Student { get; set; }

    }
}