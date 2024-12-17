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
    public class DiscountCodeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DiscountCode
        public ActionResult Index(int? page)
        {
            var customerId = User.Identity.GetUserId(); // Hoặc cách lấy ID của người dùng đăng nhập tùy vào cấu trúc ứng dụng
            var items = db.DiscountCodes.Where(x => x.UserId == customerId).OrderByDescending(x => x.DiscountPercentage).ToList();

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
    }
}