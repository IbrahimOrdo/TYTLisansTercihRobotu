using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lisans_3.Models
{
    public class Class1
    {
        public IEnumerable<SelectListItem> SehirlerList { get; set; }
        public IEnumerable<SelectListItem> UniversitelerList { get; set; }
        public IEnumerable<SelectListItem> BolumlerList { get; set; }
    }
}