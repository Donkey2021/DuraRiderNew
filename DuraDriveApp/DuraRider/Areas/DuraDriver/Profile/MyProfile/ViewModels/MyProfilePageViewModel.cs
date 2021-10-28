using DuraRider.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DuraRider.Areas.DuraDriver.Profile.MyProfile.ViewModels
{
   public class MyProfilePageViewModel : AppBaseViewModel
    {
        private bool _saveDetailsIsVisible;
        public bool SaveDetailsIsVisible
        {
            get { return _saveDetailsIsVisible; }
            set { _saveDetailsIsVisible = value; OnPropertyChanged(); }
        }

        private VihicleModel _vehicleSelected;
        public VihicleModel VehicleSelected
        {
            get { return _vehicleSelected; }
            set { _vehicleSelected = value; OnPropertyChanged(); LoadData(); }
        }

        private Color _personalDetailsTextColor = Color.Black;
        public Color PersonalDetailsTextColor
        {
            get => _personalDetailsTextColor;
            set { _personalDetailsTextColor = value; OnPropertyChanged(); }
        }

        private Color _personalDetailsBoxviewColor = Color.FromHex("#267EAA");
        public Color PersonalDetailsBoxviewColor
        {
            get => _personalDetailsBoxviewColor;
            set { _personalDetailsBoxviewColor = value; OnPropertyChanged(); }
        }
        private Color _officalDetailsTextColor = Color.Gray;
        public Color OfficalDetailsTextColor
        {
            get => _officalDetailsTextColor;
            set { _officalDetailsTextColor = value; OnPropertyChanged(); }
        }

        private Color _officalDetailsBoxviewColor = Color.WhiteSmoke;
        public Color OfficalDetailsBoxviewColor
        {
            get => _officalDetailsBoxviewColor;
            set { _officalDetailsBoxviewColor = value; OnPropertyChanged(); }
        }

        private Color _paymentDetailsTextColor = Color.Gray;
        public Color PaymentDetailsTextColor
        {
            get => _paymentDetailsTextColor;
            set { _paymentDetailsTextColor = value; OnPropertyChanged(); }
        }

        private Color _paymentDetailsBoxviewColor = Color.WhiteSmoke;
        public Color PaymentDetailsBoxviewColor
        {
            get => _paymentDetailsBoxviewColor;
            set { _paymentDetailsBoxviewColor = value; OnPropertyChanged(); }
        }


        private bool _personalDetailsIsVisible;
        public bool PersonalDetailsIsVisible
        {
            get { return _personalDetailsIsVisible; }
            set { _personalDetailsIsVisible = value; OnPropertyChanged(); }
        }
        private bool _personalDetailsEditButtonIsVisible;
        public bool PersonalDetailsEditButtonIsVisible
        {
            get { return _personalDetailsEditButtonIsVisible; }
            set { _personalDetailsEditButtonIsVisible = value; OnPropertyChanged(); }
        }

        private bool _officialDetailsIsVisible;
        public bool OfficialDetailsIsVisible
        {
            get { return _officialDetailsIsVisible; }
            set { _officialDetailsIsVisible = value; OnPropertyChanged(); }
        }


        private bool _paymentDetailsIsVisible;
        public bool PaymentDetailsIsVisible
        {
            get { return _paymentDetailsIsVisible; }
            set { _paymentDetailsIsVisible = value; OnPropertyChanged(); }
        }

        private bool _imageIsVisible;
        public bool ImageIsVisible
        {
            get { return _imageIsVisible; }
            set { _imageIsVisible = value; OnPropertyChanged(nameof(EditBtn)); }
        }
        private string _editBtn;
        public string EditBtn
        {
            get { return _editBtn; }
            set { _editBtn = value; OnPropertyChanged(nameof(EditBtn)); }
        }
        public class VihicleModel
        {
            public string Name { get; set; }
        }

        private bool _isReadFirstName;
        public bool IsReadFirstName
        {
            get { return _isReadFirstName; }
            set { _isReadFirstName = value; OnPropertyChanged(nameof(IsReadFirstName)); }
        }
        private bool _isReadMiddleName;
        public bool IsReadMiddleName
        {
            get { return _isReadMiddleName; }
            set { _isReadMiddleName = value; OnPropertyChanged(nameof(IsReadMiddleName)); }
        }
        private bool _isReadLastName;
        public bool IsReadLastName
        {
            get { return _isReadLastName; }
            set { _isReadLastName = value; OnPropertyChanged(nameof(IsReadLastName)); }
        }
        private bool _isReadMobileText;
        public bool IsReadMobileText
        {
            get { return _isReadMobileText; }
            set { _isReadMobileText = value; OnPropertyChanged(nameof(IsReadMobileText)); }
        }
        private bool _isReadEmailText;
        public bool IsReadEmailText
        {
            get { return _isReadEmailText; }
            set { _isReadEmailText = value; OnPropertyChanged(nameof(IsReadEmailText)); }
        }
        private bool _isEnabledDOBText;
        public bool IsEnabledDOBText
        {
            get { return _isEnabledDOBText; }
            set { _isEnabledDOBText = value; OnPropertyChanged(nameof(IsEnabledDOBText)); }
        }
        private bool _isReadFaceBookText;
        public bool IsReadFaceBookText
        {
            get { return _isReadFaceBookText; }
            set { _isReadFaceBookText = value; OnPropertyChanged(nameof(IsReadFaceBookText)); }
        }

        private bool _isReadGoogleText;
        public bool IsReadGoogleText
        {
            get { return _isReadGoogleText; }
            set { _isReadGoogleText = value; OnPropertyChanged(nameof(IsReadGoogleText)); }
        }

        private bool _isReadMngDetailText;
        public bool IsReadMngDetailText
        {
            get { return _isReadMngDetailText; }
            set { _isReadMngDetailText = value; OnPropertyChanged(nameof(IsReadMngDetailText)); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }


        private string _middleName;
        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; OnPropertyChanged(nameof(MiddleName)); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        private string _mobileText;
        public string MobileText
        {
            get { return _mobileText; }
            set { _mobileText = value; OnPropertyChanged(nameof(MobileText)); }
        }

        private string _emailText;
        public string EmailText
        {
            get { return _emailText; }
            set { _emailText = value; OnPropertyChanged(nameof(EmailText)); }
        }

        private DateTime _dOBText;
        public DateTime DOBText
        {
            get { return _dOBText; }
            set { _dOBText = value; OnPropertyChanged(nameof(DOBText)); }
        }

        private string _faceBookText;
        public string FaceBookText
        {
            get { return _faceBookText; }
            set { _faceBookText = value; OnPropertyChanged(nameof(FaceBookText)); }
        }

        private string _googleText;
        public string GoogleText
        {
            get { return _googleText; }
            set { _googleText = value; OnPropertyChanged(nameof(GoogleText)); }
        }
        private string _mngDetailText;
        public string MngDetailText
        {
            get { return _mngDetailText; }
            set { _mngDetailText = value; OnPropertyChanged(nameof(MngDetailText)); }
        }

        public MyProfilePageViewModel()
        {
            PersonalDetailsIsVisible = true;
            PersonalDetailsEditButtonIsVisible = true;
            OfficialDetailsIsVisible = false;
            PaymentDetailsIsVisible = false;
            PersonalDetailsTextColor = Color.White;
            PersonalDetailsBoxviewColor = Color.FromHex("#211E66");
            OfficalDetailsBoxviewColor = Color.Transparent;
            OfficalDetailsTextColor = Color.FromHex("#75747F");
            PaymentDetailsBoxviewColor = Color.Transparent;
            PaymentDetailsTextColor = Color.FromHex("#75747F");
            EditBtn = "Edit";
            ImageIsVisible = true;
            IsReadFaceBookText = true;
            IsReadGoogleText = true;
            IsReadEmailText = true;
            IsReadLastName = true;
            IsReadMiddleName = true;
            IsReadFirstName = true;
            IsReadMngDetailText = true;
            IsReadMobileText = true;
            IsEnabledDOBText = false;


        }
        private void LoadData()
        {
            if (VehicleSelected != null)
            {
                EditBtn = "Save";
                ImageIsVisible = false;
            }
            else
            {
                EditBtn = "Edit";
                ImageIsVisible = true;
            }
        }
        public ICommand PasswordChangeCommand => new Command(async (obj) =>
        {
            // await RichNavigation.PushAsync(new ChangePasswordPage(), typeof(ChangePasswordPage));
        });
        public ICommand EditCommand => new Command(async (obj) =>
        {
            SaveDetailsIsVisible = true;
            IsReadEmailText = false;
            IsReadFaceBookText = false;
            IsReadGoogleText = false;
            IsReadLastName = false;
            IsReadMiddleName = false;
            IsReadFirstName = false;
            IsReadMngDetailText = false;
            IsReadMobileText = false;
            IsEnabledDOBText = true;

            //PersonalDetailsIsVisible = false;
            PersonalDetailsEditButtonIsVisible = false;
        });
        public ICommand SaveCommand => new Command(async (obj) =>
        {
            // PersonalDetailsIsVisible = true;
            PersonalDetailsEditButtonIsVisible = true;
            IsReadEmailText = false;
            IsReadFaceBookText = true;
            IsReadEmailText = true;
            IsReadGoogleText = true;
            IsReadLastName = true;
            IsReadMiddleName = true;
            IsReadFirstName = true;
            IsReadMngDetailText = true;
            IsReadMobileText = true;
            IsEnabledDOBText = false;
            SaveDetailsIsVisible = false;
        });
        public ICommand EditDocumentCommand => new Command(async (obj) =>
        {
            EditBtn = "Edit";
            ImageIsVisible = true;
        });
        public ICommand AddAnotherBankCommand => new Command(async (obj) =>
        {
            //await RichNavigation.PushAsync(new EditProfilePage(), typeof(EditProfilePage));
        });
        public ICommand Tab1Command => new Command(async (obj) =>
        {
            PersonalDetailsIsVisible = true;
            PersonalDetailsEditButtonIsVisible = true;
            OfficialDetailsIsVisible = false;
            PaymentDetailsIsVisible = false;
            PersonalDetailsTextColor = Color.White;
            PersonalDetailsBoxviewColor = Color.FromHex("#211E66");

            OfficalDetailsBoxviewColor = Color.Transparent;
            OfficalDetailsTextColor = Color.FromHex("#75747F");

            PaymentDetailsBoxviewColor = Color.Transparent;
            PaymentDetailsTextColor = Color.FromHex("#75747F");
            ImageIsVisible = true;
        });
        public ICommand Tab2Command => new Command(async (obj) =>
        {
            PersonalDetailsIsVisible = false;
            // PersonalDetailsEditButtonIsVisible = false;
            SaveDetailsIsVisible = false;
            OfficialDetailsIsVisible = true;
            PaymentDetailsIsVisible = false;
            PersonalDetailsTextColor = Color.FromHex("#75747F");
            PersonalDetailsBoxviewColor = Color.Transparent;
            OfficalDetailsBoxviewColor = Color.FromHex("#211E66");
            OfficalDetailsTextColor = Color.White;
            PaymentDetailsTextColor = Color.FromHex("#75747F"); ;
            PaymentDetailsBoxviewColor = Color.Transparent;
            ImageIsVisible = true;
        });
        public ICommand Tab3Command => new Command(async (obj) =>
        {
            PersonalDetailsIsVisible = false;
            // PersonalDetailsEditButtonIsVisible
            SaveDetailsIsVisible = false;
            OfficialDetailsIsVisible = false;
            PaymentDetailsIsVisible = true;
            PersonalDetailsBoxviewColor = Color.Transparent;
            PersonalDetailsTextColor = Color.FromHex("#75747F");
            OfficalDetailsBoxviewColor = Color.Transparent;
            OfficalDetailsTextColor = Color.FromHex("#75747F");
            PaymentDetailsTextColor = Color.White;
            PaymentDetailsBoxviewColor = Color.FromHex("#211E66");
            ImageIsVisible = true;
        });
        public ObservableCollection<VihicleModel> VehicleList { get; set; } = new ObservableCollection<VihicleModel>
        {
            new VihicleModel{ Name="Motorbike"},
            new VihicleModel{ Name="MPV"},
            new VihicleModel{ Name="VAN"},
            new VihicleModel{ Name="FB"},
            new VihicleModel{ Name="Aluminum"}
    };
    }
}