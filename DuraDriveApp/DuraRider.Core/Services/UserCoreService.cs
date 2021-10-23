using DuraRider.Core.Models.Result;
using DuraRider.Core.Services;
using DuraRider.Core.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DuraRider.Core.Services
{
    public class UserCoreService : IUserCoreService
    {
        private readonly HttpService _httpService;
        public UserCoreService(HttpService httpService)
        {
            _httpService = httpService;
        }

        //public async Task<Result<BaseApiIdResponseModel>> AddEmergencyContact(MultipartFormDataContent request)
        //{
        //    var response = await _httpService.PostJsonAsync<BaseApiIdResponseModel>(Urls.BASE_URL + Urls.ADD_EMERGENCY_Url, request);
        //    if (response?.ResultType == ResultType.Ok)
        //    {
        //        if (response?.Data != null)
        //        {
        //            //App.Locator.CurrentUser.AppointmentData = response?.Data;
        //        }
        //    }
        //    return response;
        //}


        //public async Task<Result<GetProductsResponseModel>> GetProducts()
        //{
        //    var response = await _httpService.GetJsonAsync<GetProductsResponseModel>(Urls.BASE_URL + Urls.GET_PRODUCTS_URL);
        //    if (response?.ResultType == ResultType.Ok)
        //    {
        //        if (response?.Data != null)
        //        {
        //            //App.Locator.CurrentUser.AppointmentData = response?.Data;
        //        }
        //    }
        //    return response;
        //}
    }
}
