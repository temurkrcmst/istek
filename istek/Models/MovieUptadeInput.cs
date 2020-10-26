using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace istek.Actions
{
    public class MovieUptadeInput
    {
        public string FilterVal { get; set; } // tek bir sorgu ile sorgu işlemi yapmadan dinamik olarak filtreleme işlemi yaparız BsonValue ile, kolona ait değeri otomatik olarak alır
        public string FilterCol { get; set; }
        public string Id { get; set; }
        public string MovieName { get; set; }
        public string MovieType { get; set; }
        public double Time { get; set; }
    }
}