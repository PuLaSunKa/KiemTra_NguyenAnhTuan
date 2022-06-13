using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenAnhTuan.Models;
namespace KiemTra_NguyenAnhTuan.Controllers
{  
    public class SinhVienController : Controller
    {
        // GET: SinhVien
        KiemTraDataContext data = new KiemTraDataContext();
        public ActionResult Index()
        {
            List<SinhVien> lst = data.SinhViens.ToList();
            return View(lst);
        }
        public ActionResult Create()
        {
            ViewBag.MaNganh = new SelectList(data.NganhHocs.ToList().OrderBy(n => n.TenNganh), "MaNganh", "TenNganh");
            return View();
        }
        [HttpPost]
        public ActionResult Create(SinhVien sv)
        {
            data.SinhViens.InsertOnSubmit(sv);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
        public ActionResult Edit(string id)
        {
            var ESV = data.SinhViens.First(m => m.MaSV == id);
            ViewBag.MaNganh = new SelectList(data.NganhHocs.ToList().OrderBy(n => n.TenNganh), "MaNganh", "TenNganh");
            return View(ESV);
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult XacNhanEdit(string id)
        {
            var sv = data.SinhViens.First(m => m.MaSV == id);
            UpdateModel(sv);
            data.SubmitChanges();
            return RedirectToAction("Index");           
        }
        public ActionResult Delete(string id)
        {
            var D_sach = data.SinhViens.First(m => m.MaSV == id);
            return View(D_sach);
        }
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var DSV = data.SinhViens.Where(m => m.MaSV == id).First();
            data.SinhViens.DeleteOnSubmit(DSV);
            data.SubmitChanges();
            return RedirectToAction("ListSach");
        }
        public ActionResult Detail(string id)
        {
            var DSV = data.SinhViens.Where(m => m.MaSV == id).First();
            return View(DSV);
        }
    }
}