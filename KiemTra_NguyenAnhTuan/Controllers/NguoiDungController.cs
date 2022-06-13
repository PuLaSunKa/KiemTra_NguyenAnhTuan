using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenAnhTuan.Models;
namespace KiemTra_NguyenAnhTuan.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: DangNhap
        KiemTraDataContext data = new KiemTraDataContext();
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            var dn = f["txtMaSV"];      
            if (String.IsNullOrEmpty(dn))
                ViewData["Loi1"] = "Vui lòng nhập mã số sinh viên";       
            else
            {
                var tk = data.SinhViens.SingleOrDefault(n => n.MaSV == dn );
                if (tk != null)
                {
                    Session["Taikhoan"] = tk;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Mã số sinh viên đăng nhập không hợp lệ";
            }
            return View();

        }
    }
}