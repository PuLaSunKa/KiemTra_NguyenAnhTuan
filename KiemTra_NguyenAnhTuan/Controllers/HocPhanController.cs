using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_NguyenAnhTuan.Models;
namespace KiemTra_NguyenAnhTuan.Controllers
{
    public class HocPhanController : Controller
    {
        // GET: HocPhan
        KiemTraDataContext data = new KiemTraDataContext();
        public ActionResult Index()
        {
            List<HocPhan> lst = data.HocPhans.ToList();
            return View(lst);
        }
    }
}