using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenAnhTuan.Models;

namespace KiemTra_NguyenAnhTuan.Controllers
{
    public class DangKyController : Controller
    {
        // GET: DangKy
        KiemTraDataContext data = new KiemTraDataContext();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                //Neu gio hang chua ton tai thi khoi tao listGiohang
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
        //Them học phần vao gio
        public ActionResult ThemGioHang(string id, string strURL)
        {
            //Lay ra Session gio hang
            List<GioHang> lstGiohang = LayGioHang();
            //Kiem tra sách này tồn tại trong Session["Giohang"] chưa?
            GioHang sanpham = lstGiohang.Find(n => n.sMahocphan == id);
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                ViewData["Cảnh báo"] = "Học phần đã có trong danh mục";
                return Redirect(strURL);
            }
        }
        //Tong so luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSotinchi);
            }
            return iTongSoLuong;
        }
        private int TongSoLuongHocPhan()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Count();
            }
            return iTongSoLuong;
        }
        //HIen thi giỏ hàng.
        public ActionResult GioHang()
        {
            List<GioHang> lstGiohang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();                      
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuongHocPhan();
            return PartialView();
        }
        //Xoa Giohang
        public ActionResult XoaGioHang(string id)
        {
            //Lay gio hang tu Session
            List<GioHang> lstGiohang = LayGioHang();
            //Kiem tra sach da co trong Session["Giohang"]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.sMahocphan == id);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.sMahocphan == id);
                return RedirectToAction("GioHang");
            }          
            return RedirectToAction("GioHang");
        }
        //Xóa tất cả giỏ hàng
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = LayGioHang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }
        //Cap nhat Giỏ hàng
        /*public ActionResult CapNhatGioHang(string id, FormCollection f)
        {

            //Lay gio hang tu Session
            List<GioHang> lstGiohang = LayGioHang();
            //Kiem tra sach da co trong Session["Giohang"]
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.sMahocphan == id);
            //Neu ton tai thi cho sua Soluong
            if (sanpham != null)
            {
                sanpham.iSotinchi = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang", "DangKy");
        }*/
        [HttpGet]
        public ActionResult LuuDangKy()
        {                 
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
                return RedirectToAction("DangNhap", "NguoiDung");
            if (Session["Giohang"] == null)
                return RedirectToAction("Index", "HocPhan");
            List<GioHang> lstGiohang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongsoluonghocphan = TongSoLuongHocPhan();
            return View(lstGiohang);
        }
        [HttpPost]
        //Xay dung chuc nang Dathang
        public ActionResult LuuDangKy(FormCollection collection)
        {           
            //thêm đăng ký
            DangKy ddk = new DangKy();
            SinhVien sv = (SinhVien)Session["Taikhoan"];
            List<GioHang> gh = LayGioHang();
            ddk.MaSV = sv.MaSV;
            ddk.NgayDK = DateTime.Now;          
            data.DangKies.InsertOnSubmit(ddk);
            data.SubmitChanges();
            //Them chi tiet dang ký         
            foreach (var item in gh)
            {
                ChiTietDangKy ctdk = new ChiTietDangKy();
                ctdk.MaDK = ddk.MaDK;
                ctdk.MaHP = item.sMahocphan;             
                data.ChiTietDangKies.InsertOnSubmit(ctdk);
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("XacNhanDangKy", "DangKy");
        }
        public ActionResult XacNhanDangKy()
        {
            return View();
        }     
    }
}