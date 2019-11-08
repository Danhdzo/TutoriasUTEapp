using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class ClassGroupCourse
    {

        [Key, Column(Order = 0)]
        [ForeignKey("ClassGroup")]
        public int ClassGroupID { get; set; }
        public ClassGroup ClassGroup { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }


    }
}