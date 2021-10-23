using System;
using System.Collections.Generic;
using System.Text;

namespace DuraRider.Core.Helpers.Enums
{
    public enum MiscellaneousEnumeration
    {
    }
    public enum AppState
    {
        Undefinded,
        Foreground,
        Background
    }
    public enum ResultType
    {
        Ok,
        Invalid,
        Unauthorized,
        InternalError,
        PartialOk,
        NotFound,
        Unexpected
    }
    public enum TransitionType
    {
        SlideFromBottom = 0,
        None = 1,
        Default = 2,
    }
    public enum SendInvite
    {
        LOGIN_WAY = 0,
        HOME_WAY = 1,
        NONE = 2
    }
    public enum PaymentWay
    {
        BILLING_WAY = 0,
        HOME_MEMBERSHIP_WAY = 1,
        NONE = 2
    }
    public enum ProfilePicSelectionType
    {
        Camera,
        Gallery
    }

    public enum DocumentType
    {
        Licence,
        PoliceClearance,
        CRNumber,
        Vehicle
    }
    public enum ImageType
    {
        Front,
        Back,
        Single
    }
    public enum ImageNameType
    {
        front_licenseImage,
        back_licenseImage,
        police_clearance_image,
        crno_image,
        vehiclephoto
    }
    public enum ImageName
    {
        vehicle_imageimage,
        Crno_imageimage,
        Driver_policeverificationimage,
        Driver_backlicensephoto,
        Driver_frontlicensephoto
    }
    public enum Doc_Type
    {
        licence,
        police,
        cr,
        vehicle
    }
    public enum Image_Type
    {
        front,
        back,
        single
    }
}
