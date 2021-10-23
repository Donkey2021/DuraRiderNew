using System;
using System.Collections.Generic;

namespace DuraRider.Core.Models.ResponseModels
{
    public class NewLocationDataResponse
    {
        public int id { get; set; }
        public string iso { get; set; }
        public string country_name { get; set; }
        public int country_code { get; set; }
    }
    public class GetAllLocationResponseModel : CommonResponseModel
    {

        public List<NewLocationDataResponse> data { get; set; }
    }
}
