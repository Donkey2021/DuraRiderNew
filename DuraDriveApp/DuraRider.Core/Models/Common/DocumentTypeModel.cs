using System;
using DuraRider.Core.Helpers.Enums;

namespace DuraRider.Core.Models.Common
{
    public class DocumentTypeModel
    {
        public DocumentType documentType { get; set; }
        public ImageNameType propertyname { get; set; }
        public ImageName imagename { get; set; }
        public bool IsFrontImageAvailable { get; set; }
        public bool IsBackImageAvailable { get; set; }
    }
}
