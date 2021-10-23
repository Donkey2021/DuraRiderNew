using System;
using System.Collections.Generic;
using System.Text;

namespace DuraRider.Core.Helpers
{
    public class Urls
    {
        // Base Url
        //public static string BASE_URL = "https://162.241.87.160/shriek_api/api/";
        public static string BASE_URL = "https://duradriver.com/duradrive/duradrive_api";
        //Auth Urls
        public static string GET_ALL_LOCATION = "/alllocation";
        public static string Signup = "/api/driverRegistration";
        public static string UpdateProfile = "/api/driverUpdateProfile";
        public static string UserDetailUpdate = "";
        public static string DriverDetail = "/api/getDriverDetails";
        public static string LoginApiUrl = "/api/driverLogin";
        public static string ServiceAreaAPIURL = "/api/driverServiceArea";
        public static string SendOTPUrl = "/api/driverSendOTP";
        public static string VerifyOTPUrl = "/api/driverVerifyOTP";
        public static string ChangepasswordUrl = "/api/driverUpdatePassword";
        public static string ApprovalStatusUrl = "/api/driver-approval-status";
        public static string DriverLicenseUploadUrl = "/api/driverLicenseUpload";
        public static string DriverPersonalInfoUrl = "/api/driverPersonalInfoUpload";
        public static string ClearanceNoUrl = "/api/driverClearanceUpload";
        public static string VehicleUrl = "/api/driverVehicleUpload";
        public static string BankUrl = "/api/driverGCashdetailUpload";
        public static string ProfilePicUrl = "/api/driverProfilePicUpload";
        public static string NewDriverDocsUpload = "/api/driverdocUpoad";
        public static string DuraBagId = "/api/driverDurabagIDUpload";
        //User

        //Notification
    }
}
