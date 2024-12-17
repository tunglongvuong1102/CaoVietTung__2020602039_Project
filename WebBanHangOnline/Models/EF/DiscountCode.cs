using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    public class DiscountCode 
    {
        public int Id { get; set; }
        public string Code { get; set; } // Mã giảm giá (dạng string)
        public int DiscountPercentage { get; set; } // Phần trăm giảm giá
        public string UserId { get; set; } // Liên kết đến User
        public bool IsUsed { get; set; } // Trạng thái đã dùng hay chưa
        public DateTime ExpiryDate { get; set; } // Hạn sử dụng
    }
}