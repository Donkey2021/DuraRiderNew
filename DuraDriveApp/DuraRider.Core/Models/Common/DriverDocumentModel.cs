using System;
using DuraRider.Core.Helpers.Enums;

namespace DuraRider.Core.Models.Common
{
    public class DriverDocumentModel
    {
        public Doc_Type doc_type { get; set; }
        public Image_Type image_type { get; set; }
        public string docs_no { get; set; }
        public string vehicle_name { get; set; }
        public ImageName imagename { get; set; }
    }
}
