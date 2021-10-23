using System;
namespace DuraRider.Core.Models.ResponseModels
{
    public class DriverLicenseResponseModel : CommonResponseModel
    {
        public LicenseData data { get; set; }
    }
    public class DriverLicenseData
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string mobile { get; set; }
        public string dob { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string manager_account_no { get; set; }
        public string g_cash_accont_name { get; set; }
        public string g_cash_no { get; set; }
        public string lastupdatedatetime { get; set; }
        public string profilephoto_url { get; set; }
        public string qr_code { get; set; }
        public string dura_bag_id { get; set; }
        public object vehicle_id { get; set; }
        public string created_datetime { get; set; }
        public string isactive { get; set; }
        public string isvarified { get; set; }
        public string is_online { get; set; }
        public string is_autoaccept { get; set; }
        public string referralcode { get; set; }
        public string refered_by { get; set; }
        public string isbusinessaccout { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
    }

    public class DriverLicenseDoc
    {
        public int id { get; set; }
        public int driver_id { get; set; }
        public string cr_no { get; set; }
        public string crno_image { get; set; }
        public string licence_no { get; set; }
        public string frontlicensephoto { get; set; }
        public string backlicensephoto { get; set; }
        public string police_clearance_no { get; set; }
        public string police_clearance_image { get; set; }
        public string vehicle_id { get; set; }
        public string vehiclephoto { get; set; }
        public string vehicle_name { get; set; }
        public string document_type { get; set; }
        public string docsExpire { get; set; }
        public string docs_status { get; set; }
        public string document_file { get; set; }
        public string cardtypeid { get; set; }
        public string isactive { get; set; }
        public string createddate { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class DriverLicenseWallet
    {
        public int id { get; set; }
        public string driver_id { get; set; }
        public string wallet_amount { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class LicenseData
    {
        public DriverLicenseData driverData { get; set; }
        public DriverLicenseDoc driverDoc { get; set; }
        public DriverLicenseWallet driverWallet { get; set; }
    }
}
