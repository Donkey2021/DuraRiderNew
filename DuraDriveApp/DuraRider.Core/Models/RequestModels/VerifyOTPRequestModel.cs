using System;
namespace DuraRider.Core.Models.RequestModels
{
    public class VerifyOTPRequestModel
    {
        public string country_code { get; set; }
        public string mobile { get; set; }
        public string otp { get; set; }
    }
}
