using System;
using System.Collections.Generic;

namespace DuraRider.Core.Models.ResponseModels
{
    public class ServiceAreaResponseModel : CommonResponseModel
    {
        public ServiceData data { get; set; }
    }
    public class AreaData
    {
        public int id { get; set; }
        public string service_name { get; set; }
        public string country { get; set; }
        public string area { get; set; }
        public string timezone { get; set; }
        public string vehicle_service { get; set; }
        public string driver_doc { get; set; }
        public string vehicle_doc { get; set; }
        public string driver_minbal { get; set; }
        public string delivery_area { get; set; }
        public string max_tollprice { get; set; }
        public string bill_method { get; set; }
        public string latitute { get; set; }
        public string longitute { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class ServiceData
    {
        public List<AreaData> areaData { get; set; }
    }

}
