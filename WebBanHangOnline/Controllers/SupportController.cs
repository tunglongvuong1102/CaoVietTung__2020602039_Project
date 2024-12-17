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
    public class SupportController : Controller
    {
        // GET: Support
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Support (Hiển thị danh sách yêu cầu hỗ trợ)
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var supports = db.Supports.OrderByDescending(s => s.CreateDate).ToPagedList(pageNumber, pageSize);

            ViewBag.Page = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(supports);
        }

        // GET: Support/Create (Hiển thị form tạo yêu cầu hỗ trợ)
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Support model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = User.Identity.GetUserId(); // Lấy UserId từ người dùng hiện tại
                model.CreateDate = DateTime.Now;
                model.Status = "Chờ phản hồi";

                db.Supports.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Respond(int id, string responseContent)
        {
            var support = db.Supports.Find(id);
            if (support != null)
            {
                support.Respond = responseContent;
                support.Status = "Đã phản hồi";
                support.ModifiedDate = DateTime.Now;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        // GET: Support/Edit/{id} (Hiển thị form chỉnh sửa phản hồi)
        public ActionResult Edit(int id)
        {
            var support = db.Supports.Find(id);
            if (support == null) return HttpNotFound();
            return View(support);
        }

        // POST: Support/Edit/{id} (Cập nhật phản hồi)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Support model)
        {
            if (ModelState.IsValid)
            {
                var support = db.Supports.Find(model.Id);
                if (support == null) return HttpNotFound();

                support.Respond = model.Respond;
                support.Status = "Đã phản hồi";
                support.ModifiedDate = DateTime.Now;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}