using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace istek.Models
{
    public class CompanyUptadeInput
    {
        public string FilterCol { get; set; }
        public string FilterVal { get; set; }
        public string CompanyID { get; set; }
        public string CompanyName { get; set; }
    }
}