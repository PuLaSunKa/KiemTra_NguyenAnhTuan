using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KiemTra_NguyenAnhTuan.Models;
namespace KiemTra_NguyenAnhTuan.Models
{
    public class GioHang
    {
        KiemTraDataContext data = new KiemTraDataContext();
        public string sMahocphan { set; get; }
        public string sTenhocphan { set; get; }
        public int iSotinchi { set; get; }

        //Khoi tao gio hàng theo Masach duoc truyen vao voi Soluong mac dinh la 1
        public GioHang(string MaHP)
        {
            sMahocphan = MaHP;
            HocPhan hp = data.HocPhans.Single(n => n.MaHP == sMahocphan);
            sTenhocphan = hp.TenHP;
            iSotinchi = (int)hp.SoTinChi;
        }
    }
}