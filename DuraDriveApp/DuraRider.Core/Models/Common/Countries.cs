using System;
namespace DuraRider.Core.Models.Common
{
    public class Countries
    {
        public string name { get; set; }
        public string dial_code { get; set; }
        public string code { get; set; }
    }
    public class CurrentLocation
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string CurrentLocationName
        {
            get
            {
                return $"{City}, {State}, {Country}";
            }
        }
    }
}
