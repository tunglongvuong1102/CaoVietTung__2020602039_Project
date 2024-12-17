using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    public class Support
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Status { get; set; } // "Chờ phản hồi" hoặc "Đã phản hồi"
        public string Respond { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; } // Nullable để ban đầu có thể không có giá trị
        public string UserId { get; set; } // Mã khách hàng
        public virtual ApplicationUser User { get; set; } // Điều hướng đến bảng AspNetUsers
    }
}
