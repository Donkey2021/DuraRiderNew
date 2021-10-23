using System;
using Xamarin.Android.Net;
using Xamarin.Forms;
using Android.Net;
using Javax.Net.Ssl;
using System.Net.Http;
using DuraRider.Droid.Services;
using DuraRider.Services;

[assembly: Dependency(typeof(HTTPClientHandlerCreationService_Android))]
namespace DuraRider.Droid.Services
{
    public class HTTPClientHandlerCreationService_Android : IHTTPClientHandlerCreationService
    {
        HttpClientHandler IHTTPClientHandlerCreationService.GetInsecureHandler()
        {
            return new IgnoreSSLClientHandler();
        }

        internal class IgnoreSSLClientHandler : AndroidClientHandler
        {
            protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
            {
                return SSLCertificateSocketFactory.GetInsecure(1000, null);
            }

            protected override IHostnameVerifier GetSSLHostnameVerifier(HttpsURLConnection connection)
            {
                return new IgnoreSSLHostnameVerifier();
            }
        }

        internal class IgnoreSSLHostnameVerifier : Java.Lang.Object, IHostnameVerifier
        {
            public bool Verify(string hostname, ISSLSession session)
            {
                return true;
            }
        }
    }
}
