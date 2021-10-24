
using System.Net.Http;
using System.Threading.Tasks;
using DuraRider.Core.Models.RequestModels;
using DuraRider.Core.Models.ResponseModels;
using DuraRider.Core.Models.Result;

namespace DuraRider.Core.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Result<SendOTPResponseModel>> SendOTPService(MultipartFormDataContent request);
        Task<Result<VerifyOTPResponseModel>> VerifyOTPService(MultipartFormDataContent request);
        Task<Result<DriverLoginResponseModel>> DriverRegistrationService(MultipartFormDataContent request);
        Task<Result<GetAllLocationResponseModel>> GetAllLocationsNew();
        Task<Result<DriverPersonalInfoResponseModel>> SaveDriverPersonalInfo(MultipartFormDataContent request);
        Task<Result<DriverLicenseResponseModel>> SaveDriverLicenceDocuments(MultipartFormDataContent request);
        Task<Result<PoliceVerificationResponseModel>> SaveDriverPoliceDocuments(MultipartFormDataContent request);
        Task<Result<PoliceVerificationResponseModel>> SaveDriverClearanceDocuments(MultipartFormDataContent request);
        Task<Result<PoliceVerificationResponseModel>> SaveDriverVehicleDocuments(MultipartFormDataContent request);
        Task<Result<DriverDocumentResponseModel>> SaveProfilePic(MultipartFormDataContent request);
        Task<Result<DriverDocumentResponseModel>> SaveDriverDocument(MultipartFormDataContent request);
        Task<Result<DriverDocumentResponseModel>> SaveDriverGCashAccountDetails(MultipartFormDataContent request);
        Task<Result<DriverDocumentResponseModel>> SaveDriverDuraBag(MultipartFormDataContent request);
        Task<Result<ServiceAreaResponseModel>> GetServiceArea(MultipartFormDataContent request);
        Task<Result<DriverLoginResponseModel>> DriverLogin(DriverLoginRequestModel request);
    }
}
