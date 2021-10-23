using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DuraRider.Core.Helpers;
using DuraRider.Core.Helpers.Enums;
using DuraRider.Core.Models.Auth;
using DuraRider.Core.Models.Common;
using DuraRider.Core.Services.Interfaces;
using DuraRider.Helpers;
using DuraRider.Services.Interfaces;
using DuraRider.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace DuraRider.Areas.Common.ViewModels
{
    public class DocumentPageViewModel : AppBaseViewModel
    {
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        byte[] FrontimageByteArray;
        byte[] BackimageByteArray;
        byte[] PoliceverifyimageByteArray;
        byte[] ClearanceNoByteArray;
        byte[] VehicleByteArray;
        public IAsyncCommand FrontlicenseTap { get; set; }
        public IAsyncCommand BacklicenseTap { get; set; }
        public IAsyncCommand PoliceClearanceTap { get; set; }
        public IAsyncCommand CRTapped { get; set; }
        public IAsyncCommand VehicleTapped { get; set; }
        public IAsyncCommand NextCommand { get; set; }
        public ObservableCollection<string> VehicleList { get; set; } = new ObservableCollection<string>
        {
            "Motorbike","MPV","VAN","FB","Aluminum"
        };


        private string _selectedVehicle;
        public string SelectedVehicle
        {
            get { return _selectedVehicle; }
            set
            {
                _selectedVehicle = value;
                OnPropertyChanged(nameof(SelectedVehicle));
            }
        }

        private string _licenceNo;
        public string LicenceNumber
        {
            get { return _licenceNo; }
            set
            {
                _licenceNo = value;
                OnPropertyChanged(nameof(LicenceNumber));
            }
        }

        private string _policeDocNumber;
        public string PoliceDocNumber
        {
            get { return _policeDocNumber; }
            set
            {
                _policeDocNumber = value;
                OnPropertyChanged(nameof(PoliceDocNumber));
            }
        }

        private string _cRNo;
        public string CRNo
        {
            get { return _cRNo; }
            set
            {
                _cRNo = value;
                OnPropertyChanged(nameof(CRNo));
            }
        }

        private string _vehicleType;
        public string VehicleType
        {
            get { return _vehicleType; }
            set
            {
                _vehicleType = value;
                OnPropertyChanged(nameof(VehicleType));
            }
        }

        private ImageSource _frontLicenseSource;
        public ImageSource FrontLicenseSource
        {
            get { return _frontLicenseSource; }
            set { _frontLicenseSource = value; OnPropertyChanged(nameof(FrontLicenseSource)); }
        }

        private ImageSource _backLicenseSource;
        public ImageSource BackLicenseSource
        {
            get { return _backLicenseSource; }
            set { _backLicenseSource = value; OnPropertyChanged(nameof(BackLicenseSource)); }
        }

        private ImageSource _policeClearance_image;
        public ImageSource PoliceClearance_image
        {
            get { return _policeClearance_image; }
            set { _policeClearance_image = value; OnPropertyChanged(nameof(PoliceClearance_image)); }
        }

        private ImageSource _clearanceNo_image;
        public ImageSource ClearanceNo_image
        {
            get { return _clearanceNo_image; }
            set { _clearanceNo_image = value; OnPropertyChanged(nameof(ClearanceNo_image)); }
        }

        private ImageSource _vehicle_image;
        public ImageSource Vehicle_image
        {
            get { return _vehicle_image; }
            set { _vehicle_image = value; OnPropertyChanged(nameof(Vehicle_image)); }
        }

        private RegistrationData _registrationData;
        public RegistrationData RegistrationData
        {
            get { return _registrationData; }
            set
            {
                _registrationData = value;
                OnPropertyChanged(nameof(RegistrationData));
            }
        }

        private string _referralCode;
        public string ReferralCode
        {
            get { return _referralCode; }
            set
            {
                _referralCode = value;
                OnPropertyChanged(nameof(ReferralCode));
            }
        }

        private bool _isLicenceFrontImageVisible = true;
        public bool IsLicenceFrontImageVisible
        {
            get { return _isLicenceFrontImageVisible; }
            set
            {
                _isLicenceFrontImageVisible = value;
                OnPropertyChanged(nameof(IsLicenceFrontImageVisible));
            }
        }

        private bool _isLicenceBackImageVisible = true;
        public bool IsLicenceBackImageVisible
        {
            get { return _isLicenceBackImageVisible; }
            set
            {
                _isLicenceBackImageVisible = value;
                OnPropertyChanged(nameof(IsLicenceBackImageVisible));
            }
        }

        private bool _isPoliceImageVisible = true;
        public bool IsPoliceImageVisible
        {
            get { return _isPoliceImageVisible; }
            set
            {
                _isPoliceImageVisible = value;
                OnPropertyChanged(nameof(IsPoliceImageVisible));
            }
        }

        private bool _isCRImageVisible = true;
        public bool IsCRImageVisible
        {
            get { return _isCRImageVisible; }
            set
            {
                _isCRImageVisible = value;
                OnPropertyChanged(nameof(IsCRImageVisible));
            }
        }

        private bool _isVehicleImageVisible = true;
        public bool IsVehicleImageVisible
        {
            get { return _isVehicleImageVisible; }
            set
            {
                _isVehicleImageVisible = value;
                OnPropertyChanged(nameof(IsVehicleImageVisible));
            }
        }

        public DocumentPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            FrontlicenseTap = new AsyncCommand(FrontlicenseTapExecute, allowsMultipleExecutions: false);
            BacklicenseTap = new AsyncCommand(BacklicenseTapExecute, allowsMultipleExecutions: false);
            PoliceClearanceTap = new AsyncCommand(PoliceClearanceTapExecute, allowsMultipleExecutions: false);
            CRTapped = new AsyncCommand(CRTappedExecute, allowsMultipleExecutions: false);
            VehicleTapped = new AsyncCommand(VehicleTappedExecute, allowsMultipleExecutions: false);
            NextCommand = new AsyncCommand(NextCommandExecute, allowsMultipleExecutions: false);
            SelectedVehicle = VehicleList.First();
            RegistrationData = App.Locator.ReferralPopUpPage.RegistrationData;

        }

        private async Task NextCommandExecute()
        {
            if (_navigationService.GetCurrentPageViewModel() != typeof(GCashAccountDetailsPageViewModel))
            {
                await _navigationService.NavigateToAsync<GCashAccountDetailsPageViewModel>();
                await App.Locator.GCashAccountDetailsPage.InitilizeData();
            }
        }

        private async Task VehicleTappedExecute()
        {
            if (string.IsNullOrEmpty(SelectedVehicle))
            {
                ShowToast("Please select vehicle");
                return;
            }
            DriverDocumentModel documentTypeModel = new DriverDocumentModel
            {
                imagename = ImageName.vehicle_imageimage,
                docs_no = string.Empty,
                doc_type = Doc_Type.vehicle,
                image_type = Image_Type.single,
                vehicle_name = SelectedVehicle
            };
            await PickImage(documentTypeModel);
        }

        private async Task CRTappedExecute()
        {
            if (string.IsNullOrEmpty(CRNo))
            {
                ShowToast("Please provide document number");
                return;
            }
            DriverDocumentModel documentTypeModel = new DriverDocumentModel
            {
                imagename = ImageName.Crno_imageimage,
                docs_no = CRNo,
                doc_type = Doc_Type.cr,
                image_type = Image_Type.single,
                vehicle_name = string.Empty
            };
            await PickImage(documentTypeModel);
        }

        private async Task PoliceClearanceTapExecute()
        {
            if (string.IsNullOrEmpty(PoliceDocNumber))
            {
                ShowToast("Please provide document number");
                return;
            }
            DriverDocumentModel documentTypeModel = new DriverDocumentModel
            {
                imagename = ImageName.Driver_policeverificationimage,
                docs_no = PoliceDocNumber,
                doc_type = Doc_Type.police,
                image_type = Image_Type.single,
                vehicle_name = string.Empty
            };
            await PickImage(documentTypeModel);
        }

        private async Task BacklicenseTapExecute()
        {
            if (string.IsNullOrEmpty(LicenceNumber))
            {
                ShowToast("Please provide licence number");
                return;
            }
            DriverDocumentModel documentTypeModel = new DriverDocumentModel
            {
                imagename = ImageName.Driver_backlicensephoto,
                docs_no = LicenceNumber,
                doc_type = Doc_Type.licence,
                image_type = Image_Type.back,
                vehicle_name = string.Empty
            };
            await PickImage(documentTypeModel);
        }

        private async Task FrontlicenseTapExecute()
        {
            if (string.IsNullOrEmpty(LicenceNumber))
            {
                ShowToast("Please provide licence number");
                return;
            }
            DriverDocumentModel documentTypeModel = new DriverDocumentModel
            {
                imagename = ImageName.Driver_frontlicensephoto,
                docs_no = LicenceNumber,
                doc_type = Doc_Type.licence,
                image_type = Image_Type.front,
                vehicle_name = string.Empty
            };
            await PickImage(documentTypeModel);
        }
        private async Task PickImage(DriverDocumentModel documentTypeModel)
        {
            var res = await ShowCameraPopup();
            if (res != null)
            {
                if (res.Item1 == ProfilePicSelectionType.Camera)
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        ShowAlert("No Camera", ":( No camera avaialble.", "OK");
                        return;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                        Directory = "Dura",
                        CompressionQuality = 92,
                        Name = "test.jpg"
                    });

                    await SetImageAndUpload(documentTypeModel, file);
                }
                else if (res.Item1 == ProfilePicSelectionType.Gallery)
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        ShowAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                        return;
                    }
                    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                        CompressionQuality = 82
                    });
                    await SetImageAndUpload(documentTypeModel, file);
                }
            }
        }
        private async Task SetImageAndUpload(DriverDocumentModel documentTypeModel, MediaFile file)
        {
            if (documentTypeModel != null)
            {
                if (documentTypeModel.doc_type == Doc_Type.licence &&
                    documentTypeModel.image_type == Image_Type.front &&
                    documentTypeModel.imagename == ImageName.Driver_frontlicensephoto)
                {
                    if (file != null)
                    {
                        IsLicenceFrontImageVisible = false;
                        FrontLicenseSource = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        });
                        FrontimageByteArray = ImageHelper.ReadToEnd(file.GetStream());

                        await AddDriverDocument(FrontimageByteArray, documentTypeModel);
                    }
                    else
                    {
                        IsLicenceFrontImageVisible = true;
                        ShowToast("error on uploading image,try again");
                    }

                }
                else if (documentTypeModel.doc_type == Doc_Type.licence &&
                    documentTypeModel.image_type == Image_Type.back &&
                    documentTypeModel.imagename == ImageName.Driver_backlicensephoto)
                {

                    if (file != null)
                    {
                        IsLicenceBackImageVisible = false;
                        BackLicenseSource = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        });
                        BackimageByteArray = ImageHelper.ReadToEnd(file.GetStream());

                        await AddDriverDocument(BackimageByteArray, documentTypeModel);
                    }
                    else
                    {
                        IsLicenceBackImageVisible = true;
                        ShowToast("error on uploading image,try again");
                    }

                }
                else if (documentTypeModel.doc_type == Doc_Type.police &&
                    documentTypeModel.image_type == Image_Type.single &&
                    documentTypeModel.imagename == ImageName.Driver_policeverificationimage)
                {


                    if (file != null)
                    {
                        IsPoliceImageVisible = false;
                        PoliceClearance_image = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        });
                        PoliceverifyimageByteArray = ImageHelper.ReadToEnd(file.GetStream());
                        await AddDriverDocument(PoliceverifyimageByteArray, documentTypeModel);
                    }
                    else
                    {
                        IsPoliceImageVisible = true;
                        ShowToast("error on uploading image,try again");
                    }

                }
                else if (documentTypeModel.doc_type == Doc_Type.cr &&
                    documentTypeModel.image_type == Image_Type.single &&
                    documentTypeModel.imagename == ImageName.Crno_imageimage)
                {

                    if (file != null)
                    {
                        IsCRImageVisible = false;
                        ClearanceNo_image = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        });
                        ClearanceNoByteArray = ImageHelper.ReadToEnd(file.GetStream());
                        await AddDriverDocument(ClearanceNoByteArray, documentTypeModel);
                    }
                    else
                    {
                        IsCRImageVisible = true;
                        ShowToast("error on uploading image,try again");
                    }
                }
                else if (documentTypeModel.doc_type == Doc_Type.vehicle &&
                    documentTypeModel.image_type == Image_Type.single &&
                    documentTypeModel.imagename == ImageName.vehicle_imageimage)
                {

                    if (file != null)
                    {
                        IsVehicleImageVisible = false;
                        Vehicle_image = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        });
                        VehicleByteArray = ImageHelper.ReadToEnd(file.GetStream());
                        await AddDriverDocument(VehicleByteArray, documentTypeModel);
                    }
                    else
                    {
                        IsVehicleImageVisible = true;
                        ShowToast("error on uploading image,try again");
                    }
                }
            }

        }
        private async Task AddDriverDocument(byte[] data, DriverDocumentModel document)
        {
            if (CheckConnection())
            {
                ShowLoading();
                try
                {
                    var currentLoc = await LocationHelpers.getCurrentPosition();
                    if (currentLoc == null)
                    {
                        ShowToast("Current location not found, try again");
                        return;
                    }

                    var driverid = RegistrationData.driverid == 0 ? 0 : RegistrationData.driverid;
                    var doc_type = string.IsNullOrEmpty(document.doc_type.ToString()) ? string.Empty : document.doc_type.ToString();
                    var image_type = string.IsNullOrEmpty(document.image_type.ToString()) ? string.Empty : document.image_type.ToString();
                    var latitude = currentLoc.Latitude == 0 ? 0 : currentLoc.Latitude;
                    var longitude = currentLoc.Longitude == 0 ? 0 : currentLoc.Longitude;
                    var docs_no = string.IsNullOrEmpty(document.docs_no) ? string.Empty : document.docs_no;
                    var vehicle_name = string.IsNullOrEmpty(document.vehicle_name) ? string.Empty : document.vehicle_name;

                    var form = new MultipartFormDataContent();
                    form.Add(new ByteArrayContent(data), "image", $"{document?.imagename.ToString()}.jpg");
                    form.Add(new StringContent(Convert.ToString(driverid)), "driver_id");
                    form.Add(new StringContent(Convert.ToString(doc_type)), "doc_type");
                    form.Add(new StringContent(Convert.ToString(image_type)), "image_type");
                    form.Add(new StringContent(Convert.ToString(latitude)), "latitude");
                    form.Add(new StringContent(Convert.ToString(longitude)), "longitude");
                    form.Add(new StringContent(Convert.ToString(docs_no)), "docs_no");
                    form.Add(new StringContent(Convert.ToString(vehicle_name)), "vehicle_name");

                    var result = await TryWithErrorAsync(_authenticationService.SaveDriverDocument(form), true, false);
                    if (result?.ResultType == ResultType.Ok && result.Data.status == 200)
                    {
                        if (document.doc_type == Doc_Type.licence && document.image_type == Image_Type.front)
                        {
                            ShowToast("Driver front licence image uploaded successfully");
                            HideLoading();
                            return;
                        }
                        if (document.doc_type == Doc_Type.licence && document.image_type == Image_Type.back)
                        {
                            ShowToast("Driver back licence image uploaded successfully");
                            HideLoading();
                            return;
                        }
                        if (document.doc_type == Doc_Type.police && document.image_type == Image_Type.single)
                        {
                            ShowToast("Police verification image uploaded successfully");
                            HideLoading();
                            return;
                        }
                        if (document.doc_type == Doc_Type.cr && document.image_type == Image_Type.single)
                        {
                            ShowToast("Clearance image uploaded successfully");
                            HideLoading();
                            return;
                        }
                        if (document.doc_type == Doc_Type.vehicle && document.image_type == Image_Type.single)
                        {
                            ShowToast("vehicle image uploaded successfully");
                            HideLoading();
                            return;
                        }
                    }
                    else
                    {
                        if (document.doc_type == Doc_Type.licence && document.image_type == Image_Type.front)
                        {
                            ShowToast("Driver front licence image uploading failed");
                            HideLoading();
                            return;
                        }
                        if (document.doc_type == Doc_Type.licence && document.image_type == Image_Type.back)
                        {
                            ShowToast("Driver back licence image uploading failed");
                            HideLoading();
                            return;
                        }
                        if (document.doc_type == Doc_Type.police && document.image_type == Image_Type.single)
                        {
                            ShowToast("Police verification image uploading failed");
                            HideLoading();
                            return;
                        }
                        if (document.doc_type == Doc_Type.cr && document.image_type == Image_Type.single)
                        {
                            ShowToast("Clearance image uploading failed");
                            HideLoading();
                            return;
                        }
                        if (document.doc_type == Doc_Type.vehicle && document.image_type == Image_Type.single)
                        {
                            ShowToast("vehicle image uploading failed");
                            HideLoading();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    HideLoading();
                }

            }
            else
                ShowToast(CommonMessages.NoInternet);
        }
        //private async Task AddLicence(byte[] data, DocumentTypeModel document)
        //{

        //    if (CheckConnection())
        //    {
        //        ShowLoading();
        //        try
        //        {
        //            var licenceno = string.IsNullOrEmpty(LicenceNumber) ? string.Empty : LicenceNumber;
        //            var driverid = RegistrationData.driverid == 0 ? 0 : RegistrationData.driverid;
        //            var latitude = RegistrationData.latitude == 0 ? 0 : RegistrationData.latitude;
        //            var longitude = RegistrationData.longitude == 0 ? 0 : RegistrationData.longitude;

        //            var form = new MultipartFormDataContent();
        //            form.Add(new StringContent(Convert.ToString(licenceno)), "license_no");
        //            form.Add(new StringContent(Convert.ToString(latitude)), "latitude");
        //            form.Add(new StringContent(Convert.ToString(longitude)), "longitude");
        //            form.Add(new StringContent(Convert.ToString(driverid)), "driver_id");

        //            if (document.IsFrontImageAvailable)
        //            {
        //                form.Add(new ByteArrayContent(data), document.propertyname.ToString(), $"{document?.imagename.ToString()}.jpg");
        //            }
        //            else
        //            {
        //                form.Add(new StringContent(string.Empty), document.propertyname.ToString());
        //            }

        //            if (document.IsBackImageAvailable)
        //            {
        //                form.Add(new ByteArrayContent(data), document.propertyname.ToString(), $"{document?.imagename.ToString()}.jpg");
        //            }
        //            else
        //            {
        //                form.Add(new StringContent(string.Empty), document.propertyname.ToString());
        //            }


        //            var result = await TryWithErrorAsync(_authenticationService.SaveDriverLicenceDocuments(form), true, false);
        //            if (result?.ResultType == ResultType.Ok && result.Data.status == 200)
        //            {
        //                if (document.IsFrontImageAvailable)
        //                {
        //                    IsLicenceFrontImageVisible = false;
        //                    ShowToast("Driver front licence image uploaded successfully");
        //                }
        //                if (document.IsBackImageAvailable)
        //                {
        //                    IsLicenceBackImageVisible = false;
        //                    ShowToast("Driver back licence image uploaded successfully");
        //                }
        //            }
        //            else
        //            {
        //                if (document.IsFrontImageAvailable)
        //                {
        //                    IsLicenceFrontImageVisible = true;
        //                    ShowToast("Driver front licence image uploading failed");
        //                }
        //                if (document.IsBackImageAvailable)
        //                {
        //                    IsLicenceBackImageVisible = true;
        //                    ShowToast("Driver back licence image uploading failed");
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        HideLoading();
        //    }
        //    else
        //        ShowToast(CommonMessages.NoInternet);

        //}
        //private async Task AddPoliceVerification(byte[] data, DocumentTypeModel document)
        //{

        //    if (CheckConnection())
        //    {
        //        ShowLoading();
        //        try
        //        {
        //            var licenceno = string.IsNullOrEmpty(PoliceDocNumber) ? string.Empty : PoliceDocNumber;
        //            var driverid = RegistrationData.driverid == 0 ? 0 : RegistrationData.driverid;
        //            var latitude = RegistrationData.latitude == 0 ? 0 : RegistrationData.latitude;
        //            var longitude = RegistrationData.longitude == 0 ? 0 : RegistrationData.longitude;

        //            var form = new MultipartFormDataContent();
        //            form.Add(new StringContent(Convert.ToString(licenceno)), "police_clearance_no");
        //            form.Add(new StringContent(Convert.ToString(latitude)), "latitude");
        //            form.Add(new StringContent(Convert.ToString(longitude)), "longitude");
        //            form.Add(new StringContent(Convert.ToString(driverid)), "driver_id");

        //            if (document.IsFrontImageAvailable)
        //            {
        //                form.Add(new ByteArrayContent(data), document.propertyname.ToString(), $"{document?.imagename.ToString()}.jpg");
        //            }
        //            else
        //            {
        //                form.Add(new StringContent(string.Empty), document.propertyname.ToString());
        //            }

        //            var result = await TryWithErrorAsync(_authenticationService.SaveDriverPoliceDocuments(form), true, false);
        //            if (result?.ResultType == ResultType.Ok && result.Data.status == 200)
        //            {
        //                IsPoliceImageVisible = false;
        //                ShowToast("Police verification image uploaded successfully");
        //            }
        //            else
        //            {
        //                IsPoliceImageVisible = true;
        //                ShowToast("Police verification image uploading failed");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //        }
        //        HideLoading();
        //    }
        //    else
        //        ShowToast(CommonMessages.NoInternet);

        //}
        //private async Task AddCRVerification(byte[] data, DocumentTypeModel document)
        //{

        //    if (CheckConnection())
        //    {
        //        ShowLoading();
        //        try
        //        {
        //            var licenceno = string.IsNullOrEmpty(CRNo) ? string.Empty : CRNo;
        //            var driverid = RegistrationData.driverid == 0 ? 0 : RegistrationData.driverid;
        //            var latitude = RegistrationData.latitude == 0 ? 0 : RegistrationData.latitude;
        //            var longitude = RegistrationData.longitude == 0 ? 0 : RegistrationData.longitude;

        //            var form = new MultipartFormDataContent();
        //            form.Add(new StringContent(Convert.ToString(licenceno)), "cr_no");
        //            form.Add(new StringContent(Convert.ToString(latitude)), "latitude");
        //            form.Add(new StringContent(Convert.ToString(longitude)), "longitude");
        //            form.Add(new StringContent(Convert.ToString(driverid)), "driver_id");

        //            if (document.IsFrontImageAvailable)
        //            {
        //                form.Add(new ByteArrayContent(data), document.propertyname.ToString(), $"{document?.imagename.ToString()}.jpg");
        //            }
        //            else
        //            {
        //                form.Add(new StringContent(string.Empty), document.propertyname.ToString());
        //            }

        //            var result = await TryWithErrorAsync(_authenticationService.SaveDriverClearanceDocuments(form), true, false);
        //            if (result?.ResultType == ResultType.Ok && result.Data.status == 200)
        //            {
        //                IsCRImageVisible = false;
        //                ShowToast("Clearance verification image uploaded successfully");
        //            }
        //            else
        //            {
        //                IsCRImageVisible = true;
        //                ShowToast("Clearance verification image uploading failed");
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        HideLoading();
        //    }
        //    else
        //        ShowToast(CommonMessages.NoInternet);

        //}
        //private async Task AddVehicleImage(byte[] data, DocumentTypeModel document)
        //{

        //    if (CheckConnection())
        //    {
        //        ShowLoading();
        //        try
        //        {
        //            var licenceno = string.IsNullOrEmpty(SelectedVehicle) ? string.Empty : SelectedVehicle;
        //            var driverid = RegistrationData.driverid == 0 ? 0 : RegistrationData.driverid;
        //            var latitude = RegistrationData.latitude == 0 ? 0 : RegistrationData.latitude;
        //            var longitude = RegistrationData.longitude == 0 ? 0 : RegistrationData.longitude;

        //            var form = new MultipartFormDataContent();
        //            form.Add(new StringContent(Convert.ToString(licenceno)), "vehicle_name");
        //            form.Add(new StringContent(Convert.ToString(latitude)), "latitude");
        //            form.Add(new StringContent(Convert.ToString(longitude)), "longitude");
        //            form.Add(new StringContent(Convert.ToString(driverid)), "driver_id");

        //            if (document.IsFrontImageAvailable)
        //            {
        //                form.Add(new ByteArrayContent(data), document.propertyname.ToString(), $"{document?.imagename.ToString()}.jpg");
        //            }
        //            else
        //            {
        //                form.Add(new StringContent(string.Empty), document.propertyname.ToString());
        //            }

        //            var result = await TryWithErrorAsync(_authenticationService.SaveDriverVehicleDocuments(form), true, false);
        //            if (result?.ResultType == ResultType.Ok && result.Data.status == 200)
        //            {
        //                IsVehicleImageVisible = false;
        //                ShowToast("Vehicle image uploaded successfully");
        //            }
        //            else
        //            {
        //                IsVehicleImageVisible = true;
        //                ShowToast("Vehicle image uploading failed");
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        HideLoading();
        //    }
        //    else
        //        ShowToast(CommonMessages.NoInternet);

        //}
        internal async Task InitilizeData()
        {
        }
    }
}
