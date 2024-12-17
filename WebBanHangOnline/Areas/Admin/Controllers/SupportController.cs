using System;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SupportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Support
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var supports = db.Supports.OrderByDescending(s => s.CreateDate).ToPagedList(pageNumber, pageSize);

            ViewBag.Page = pageNumber;
            ViewBag.PageSize = pageSize;

            return View(supports);
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