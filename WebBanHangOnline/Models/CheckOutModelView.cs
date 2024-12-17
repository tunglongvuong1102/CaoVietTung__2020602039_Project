using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Models
{
    public class CheckOutModelView
    {
        public List<DiscountCode> DiscountCodes { get; set; }
        public string SelectedDiscountCode { get; set; }
    }
}