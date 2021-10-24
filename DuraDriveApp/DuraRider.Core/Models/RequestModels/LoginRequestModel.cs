using System;
namespace DuraRider.Core.Models.RequestModels
{
    public class DriverLoginRequestModel
    {
        public string mobile { get; set; }
        public string service_id { get; set; }
        public string country_code { get; set; }
        public string password { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    }
}
