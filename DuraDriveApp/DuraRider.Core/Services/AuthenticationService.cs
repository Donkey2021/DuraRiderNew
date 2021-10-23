using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.ResponseModels;
using DuraRider.Core.Models.Result;
using DuraRider.Core.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DuraRider.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpService _httpService;
        public AuthenticationService(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<Result<DriverLoginResponseModel>> DriverRegistrationService(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<DriverLoginResponseModel>(Urls.BASE_URL + Urls.Signup, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<GetAllLocationResponseModel>> GetAllLocationsNew()
        {
            var response = await _httpService.GetJsonAsync<GetAllLocationResponseModel>(Urls.BASE_URL + Urls.GET_ALL_LOCATION);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<PoliceVerificationResponseModel>> SaveDriverClearanceDocuments(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<PoliceVerificationResponseModel>(Urls.BASE_URL + Urls.ClearanceNoUrl, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<DriverDocumentResponseModel>> SaveDriverDocument(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<DriverDocumentResponseModel>(Urls.BASE_URL + Urls.NewDriverDocsUpload, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<DriverDocumentResponseModel>> SaveDriverDuraBag(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<DriverDocumentResponseModel>(Urls.BASE_URL + Urls.DuraBagId, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<DriverDocumentResponseModel>> SaveDriverGCashAccountDetails(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<DriverDocumentResponseModel>(Urls.BASE_URL + Urls.BankUrl, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<DriverLicenseResponseModel>> SaveDriverLicenceDocuments(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<DriverLicenseResponseModel>(Urls.BASE_URL + Urls.DriverLicenseUploadUrl, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<DriverPersonalInfoResponseModel>> SaveDriverPersonalInfo(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<DriverPersonalInfoResponseModel>(Urls.BASE_URL + Urls.DriverPersonalInfoUrl, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<PoliceVerificationResponseModel>> SaveDriverPoliceDocuments(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<PoliceVerificationResponseModel>(Urls.BASE_URL + Urls.DriverLicenseUploadUrl, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<PoliceVerificationResponseModel>> SaveDriverVehicleDocuments(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<PoliceVerificationResponseModel>(Urls.BASE_URL + Urls.VehicleUrl, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<DriverDocumentResponseModel>> SaveProfilePic(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<DriverDocumentResponseModel>(Urls.BASE_URL + Urls.ProfilePicUrl, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<SendOTPResponseModel>> SendOTPService(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<SendOTPResponseModel>(Urls.BASE_URL + Urls.SendOTPUrl, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        public async Task<Result<VerifyOTPResponseModel>> VerifyOTPService(MultipartFormDataContent request)
        {
            var response = await _httpService.PostJsonAsync<VerifyOTPResponseModel>(Urls.BASE_URL + Urls.VerifyOTPUrl, request);
            if (response?.ResultType == ResultType.Ok)
            {
                if (response?.Data != null)
                {
                    //App.Locator.CurrentUser.AppointmentData = response?.Data;
                }
            }
            return response;
        }

        //public async Task<Result<BaseApiResponseModel>> ForgotPassword(ForgotPasswordRequestModel request)
        //{
        //    var jsonRequest = JsonConvert.SerializeObject(request);
        //    var response = await _httpService.PostJsonAsync<BaseApiResponseModel>(Urls.BASE_URL + Urls.FORGOT_URL, jsonRequest);
        //    if (response?.ResultType == ResultType.Ok)
        //    {
        //        if (response?.Data != null)
        //        {
        //            //App.Locator.CurrentUser.AppointmentData = response?.Data;
        //        }
        //    }
        //    return response;
        //}

        //public async Task<Result<GetServiceLevelResponseModel>> GetAllServicesLevel()
        //{
        //    var response = await _httpService.GetJsonAsync<GetServiceLevelResponseModel>(Urls.BASE_URL + Urls.GET_SERVICE_LEVEL_URL);
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
