using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.DAO;
using ShopOnline.Entities;
using PagedList.Mvc;
using PagedList;

namespace ShopOnline.Controllers
{
    public class NhanVienController : BaseController
    {
        //
        // GET: /NhanVien/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            NhanVienDao ct = new NhanVienDao();

            IQueryable<NhanVien> lstNhanVien = ct.ListNhanVien();

            return View("List", lstNhanVien.ToPagedList(pagecurrent, pagesize));
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            NhanVienDao dao = new NhanVienDao();

            NhanVien nv = dao.FindNhanVienByICode(id);

            dao.DeleteNhanVien(nv);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<int> studentRecordDeletebyId)
        {
            NhanVienDao dao = new NhanVienDao();

            foreach (var id in studentRecordDeletebyId)
            {
                NhanVien tg = dao.FindNhanVienByICode(id);
                dao.DeleteNhanVien(tg);
            }

            return RedirectToAction("List");
        }  
        public ActionResult Edit(int id)
        {
            NhanVienDao dao = new NhanVienDao();

            NhanVien nv = dao.FindNhanVienByICode(id);
            return View(nv);
        }
        public ActionResult EditAction(NhanVien nv)
        {
            if(ModelState.IsValid)
            {
                NhanVienDao dao = new NhanVienDao();
                dao.UpdateNhanVien(nv);
                return RedirectToAction("List");
            }
            return View("Edit");
        }

        public ActionResult AddAction(NhanVien nv)
        {
            if(ModelState.IsValid)
            {
                NhanVienDao dao = new NhanVienDao();
                if (!dao.checkTenDN(nv.TenDangNhap))
                {
                    Session["Ok"] = "true";
                    dao.InsertNhanVien(nv.TenNhanVien, nv.DienThoai, nv.Email, nv.TenDangNhap, nv.MatKhau, nv.PhanQuyen);
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