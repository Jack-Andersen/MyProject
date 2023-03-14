using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppBookStore.Models
{
    internal class IpInfo
    {
        public string Ip { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string CountryName { get; set; }
        public string ContinentName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
