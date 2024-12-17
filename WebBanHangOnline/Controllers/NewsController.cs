using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index(int? page)
        {
            var pageSize = 5; // Hiển thị 5 bài viết mỗi trang
            if (page == null)
            {
                page = 1;
            }
            var items = db.News.Where(x => x.IsActive).OrderByDescending(x => x.CreatedDate); // Lấy bài viết từ cơ sở dữ liệu
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1; // Xác định trang hiện tại
            var pagedItems = items.ToPagedList(pageIndex, pageSize); // Áp dụng phân trang
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(pagedItems);
        }

        public ActionResult Detail(int id)
        {
            var item = db.News.Find(id);
            return View(item);
        }
        public ActionResult Partial_News_Home()
        {
            var items = db.News.Take(3).ToList();
            return PartialView(items);
        }
    }
}