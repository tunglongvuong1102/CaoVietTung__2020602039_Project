using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using PagedList;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Globalization;
using System.Data.Entity;
using WebBanHangOnline.Models.ViewModels;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var customerId = User.Identity.GetUserId(); // Hoặc cách lấy ID của người dùng đăng nhập tùy vào cấu trúc ứng dụng
            var items = db.Orders.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.CreatedDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }



        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var userId = User.Identity.GetUserId(); // Lấy ID người dùng đang đăng nhập
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.Status = trangthai;
                db.Entry(item).Property(x => x.Status).IsModified = true;
                db.SaveChanges();
                if (trangthai == 4) // Trạng thái "Hoàn thành" là 4
                {
                    var totalAmount = item.TotalAmount; // Lấy tổng số tiền của đơn hàng
                    var user = db.Users.Find(userId);    // Tìm tài khoản khách hàng

                    if (user != null)
                    {
                        // Cộng điểm tích lũy
                        user.Points += (int)totalAmount / 1000; // Quy đổi VNĐ sang điểm (220.000 VNĐ = 220 điểm)
                        db.SaveChanges();
                    }

                    return Json(new { Success = true, Message = "Đơn hàng đã hoàn thành. Điểm tích lũy đã được cộng." });
                }
                return Json(new { message = "Success", Success = true });
                
            }
            return Json(new { message = "Unsuccess", Success = false });
            
        }

        public ActionResult MemberCard()
        {
            // Lấy thông tin người dùng đang đăng nhập
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId); // Lấy từ database (giả sử bạn dùng ApplicationDbContext)

            // Tạo ViewModel
            var model = new MembershipCardViewModel
            {
                FullName = user.FullName, // Thuộc tính FullName từ ApplicationUser
                Points = user.Points, // Thuộc tính RewardPoints từ ApplicationUser
                PointsToRedeemOptions = new List<int> { 100, 200, 300 } // Các mức quy đổi điểm
            };

            return View(model); // Truyền ViewModel vào View
        }
        [HttpPost]
        public ActionResult RedeemPoints(int pointsToRedeem)
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);

            if (user != null && user.Points >= pointsToRedeem)
            {
                // Giảm điểm tích lũy của người dùng
                user.Points -= pointsToRedeem;

                // Tạo mã giảm giá
                var discountCode = new DiscountCode
                {
                    Code = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                    DiscountPercentage = pointsToRedeem / 10, // 100 điểm = 10%
                    UserId = userId,
                    IsUsed = false,
                    ExpiryDate = DateTime.Now.AddMonths(1) // Hạn sử dụng 1 tháng
                };
                db.DiscountCodes.Add(discountCode);
                db.SaveChanges();

                return Json(new { Success = true, Message = "Quy đổi điểm thành công!", DiscountCode = discountCode.Code });
            }
            return Json(new { Success = false, Message = "Không đủ điểm để quy đổi!" });
        }



    }
}