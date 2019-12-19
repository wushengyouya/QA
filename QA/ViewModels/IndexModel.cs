using QA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QA.ViewModels
{
    public class IndexModel
    {

        public Patient CurrentUser { get; set; }
        public IEnumerable<Section> Sections { get; set; }
    }
}