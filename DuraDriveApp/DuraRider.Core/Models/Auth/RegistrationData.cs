using System;
namespace DuraRider.Core.Models.Auth
{
    public class RegistrationData
    {
        public string email { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
        public string mobile { get; set; }
        public string countrycode { get; set; }
        public string Otp { get; set; }
        public int? driverid { get; set; }
        public string token { get; set; }
        public double? longitude { get; set; }
        public double? latitude { get; set; }
        public string manger_id { get; set; }
        public string dob { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string refreal_no { get; set; }
        public bool isfromforgotpassword { get; set; }
        public string newpassword { get; set; }
    }
}
