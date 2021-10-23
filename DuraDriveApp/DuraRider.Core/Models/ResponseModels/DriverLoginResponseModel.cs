using System;
namespace DuraRider.Core.Models.ResponseModels
{
    public class Headers
    {
    }

    public class Original
    {
        public string token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }

    public class Token
    {
        public Headers headers { get; set; }
        public Original original { get; set; }
        public object exception { get; set; }
    }

    public class LoginData
    {
        public DriverData driverData { get; set; }
        public DriverDoc driverDoc { get; set; }
        public DriverWallet driverWallet { get; set; }
        public string driverRating { get; set; }
        public string todaysTotalTrip { get; set; }
        public string dutyStatus { get; set; }
        public bool autoAcceptOrder { get; set; }
        public Token token { get; set; }
    }

    public class DriverLoginResponseModel : CommonResponseModel
    {
        public LoginData data { get; set; }
    }
}
