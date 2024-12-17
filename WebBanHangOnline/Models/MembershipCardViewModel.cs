using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class MembershipCardViewModel
    {
        public string FullName { get; set; }
        public int Points { get; set; }
        public List<int> PointsToRedeemOptions { get; set; } // Các mức điểm quy đổi
    }
}