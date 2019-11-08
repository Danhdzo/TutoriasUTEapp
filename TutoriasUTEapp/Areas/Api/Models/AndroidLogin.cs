using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidLogin
    {
        public string user { get; set; }

        public string pass { get; set; }

        public int role { get; set; }
    }
}