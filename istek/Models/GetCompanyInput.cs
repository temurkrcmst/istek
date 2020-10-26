using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace istek.Models
{
    public class GetCompanyInput
    {
        public string Column { get; set; }
        public string ColumnType { get; set; }
        public string Value { get; set; }
        public string Statement { get; set; }
    }
}