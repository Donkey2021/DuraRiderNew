using Xamarin.Essentials;

namespace DuraRider.Services
{
    public class ConnectivityService
    {
        public static bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                return true;
            }
            return false;
        }
    }
}
