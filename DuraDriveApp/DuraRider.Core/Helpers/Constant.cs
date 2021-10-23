using System;
namespace DuraRider.Core.Helpers
{
    public class Constant
    {

        #region regex
        public static string EmailRegex = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
        public static string PhoneRegex = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$";
        public static string PasswordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!% *? &])[A-Za-z\d@\s$!% *? &]{8,15}$";
        #endregion

        // https://developers.google.com/maps/documentation/android-api/signup
        //public const string GOOGLE_MAPS_ANDROID_API_KEY = "AIzaSyCKE9h6S_4q9j1l9B_W1ApK9R-vkspG5tk";
        // public const string GOOGLE_MAPS_ANDROID_API_KEY = "AIzaSyAiB54Hq9ksAhUjYwdf6x7hgo4PCGfxTNA";

        // https://developers.google.com/maps/documentation/ios-sdk/start#step_4_get_an_api_key
        // public const string GOOGLE_MAPS_IOS_API_KEY = "AIzaSyCKE9h6S_4q9j1l9B_W1ApK9R-vkspG5tk";


        #region Color Code
        public static string White = "#FFFFFF";
        public static string Black = "#2A2A2A";
        public static string Gray = "#707070";
        #endregion

        public static string WithOutFont = "Bold";
        public static string WithFont = "None";
        public static string WithOutUnder = "None";
        public static string WithUnder = "Underline";

        public static string Terms_Condition = "https://162.241.87.160/duradrive/terms-and-conditions.html";
    }
}
