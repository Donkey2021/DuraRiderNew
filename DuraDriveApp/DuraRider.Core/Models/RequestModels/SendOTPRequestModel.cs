using System;
namespace DuraRider.Core.Models.RequestModels
{
    public class SendOTPRequestModel
    {
        public string otp { get; set; }
        public string message { get; set; }
        public string country_code { get; set; }
        public string mobile { get; set; }
    }
}
