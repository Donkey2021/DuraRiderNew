using System;
using System.Collections.Generic;
using System.Text;

namespace DuraRider.Areas.DuraDriver.Home.HomeModels
{
   public class OrderModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string StatusTextColor { get; set; }
        public string StatusBgColor { get; set; }
        public string Address { get; set; }
        public string Charges { get; set; }
        public string ChargesTextColor { get; set; }
        public string date { get; set; }
    }
}