using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Entities;
using ShopOnline.DAO;
using PagedList.Mvc;
using PagedList;

namespace ShopOnline.Controllers
{
    public class KhachHangController : BaseController
    {
        //
        // GET: /KhachHang/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            KhachHangDao ct = new KhachHangDao();

            IQueryable<KhachHang> lstKhachHang = ct.ListKhachHang();

            return View("List", lstKhachHang.ToPagedList(pagecurrent, pagesize));
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            KhachHangDao dao = new KhachHangDao();

            KhachHang kh = dao.FindKhachHangByICode(id);

            dao.DeleteKhachHang(kh);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<int> studentRecordDeletebyId)
        {
           KhachHangDao dao = new  KhachHangDao();

            foreach (var id in studentRecordDeletebyId)
            {
                KhachHang tg = dao.FindKhachHangByICode(id);
                dao.DeleteKhachHang(tg);
            }

            return RedirectToAction("List");
        }  
        public ActionResult Edit(int id)
        {
            KhachHangDao dao = new KhachHangDao();

            KhachHang kh = dao.FindKhachHangByICode(id);
            return View(kh);
        }
        public ActionResult EditAction(KhachHang kh)
        {
            KhachHangDao dao = new KhachHangDao();
            dao.UpdateKhachHang(kh);
            return RedirectToAction("List");
        }

        public ActionResult AddAction(KhachHang kh)
        {
            if(ModelState.IsValid)
            {
                KhachHangDao dao = new KhachHangDao();
                if(!dao.checkTenDN(kh.TenDangNhap))
                {
                    Session["Ok"] = "true";
                    dao.InsertKhachHang(kh.TenKH, kh.Email, kh.SoDienThoai, kh.MatKhau, kh.NgaySinh, kh.GioiTinh, kh.DiaChi, kh.TenDangNhap);
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.msg_err = "* Có tên đăng nhập này rồi !!!";
                    return View("Add");
                }
                
                
            }
            Session["Ok"] = "true";
            return View("Add");

        }

        
	}
}