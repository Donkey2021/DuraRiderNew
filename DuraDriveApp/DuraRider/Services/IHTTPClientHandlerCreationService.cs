using System;
using System.Net.Http;

namespace DuraRider.Services
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
