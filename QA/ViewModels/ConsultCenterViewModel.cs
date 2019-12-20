using QA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QA.ViewModels
{
    public class ConsultCenterViewModel
    {
        public Doctor CurrentDoctor { get; set; }
        public Consult Consult { get; set;}
        public int PageCount { get; set; }
    }

    public class DoctorsTwo {
        public string id { get; set; }
        public string d_name { get; set; }
    }

}