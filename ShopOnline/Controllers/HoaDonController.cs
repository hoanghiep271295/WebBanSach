using ShopOnline.DAO;
using ShopOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace ShopOnline.Controllers
{
    public class HoaDonController : BaseController
    {
        //
        // GET: /HoaDon/
       
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            HoaDonDao ct = new HoaDonDao();

            IQueryable<HoaDon> lstChiTietHoaDon = ct.ListHoaDon();

            return View("List", lstChiTietHoaDon.ToPagedList(pagecurrent, pagesize));
        }
        public ActionResult Add()
        {
            setViewbag();
            return View();
        }

        public  ActionResult getPage(out string url)
        {
            url = Request.Url.ToString();
            return RedirectToAction("Edit");
        }
        public ActionResult Delete(int id)
        {
            HoaDonDao dao = new HoaDonDao();

            HoaDon hd = dao.FindHoaDonByICode(id);

            dao.DeleteHoaDon(hd);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<int> studentRecordDeletebyId)
        {
            HoaDonDao dao = new HoaDonDao();

            foreach (var id in studentRecordDeletebyId)
            {
                HoaDon hd = dao.FindHoaDonByICode(id);
                dao.DeleteHoaDon(hd);
            }

            return RedirectToAction("List");
        } 
        public ActionResult Edit(int id)
        {
            HoaDonDao dao = new HoaDonDao();

            HoaDon hd = dao.FindHoaDonByICode(id);
            setViewbag(hd.MaKH,hd.MaNhanVien);
            
            return View(hd);
        }
        public ActionResult EditAction(HoaDon hd)
        {
            HoaDonDao dao = new HoaDonDao();
            dao.UpdateHoaDon(hd);
            //setViewbag();
            return RedirectToAction("List");
            
        }

        public ActionResult AddAction(HoaDon hd)
        {
            if(ModelState.IsValid)
            {
                
                HoaDonDao dao = new HoaDonDao();
                dao.InsertHoaDon(hd.ThoiGian, hd.MaKH, hd.MaNhanVien);
                return RedirectToAction("List");
            }
            setViewbag();
            return View("Add");
        }

        //private void setViewbag()
        //{

        //    KhachHangDao kh = new KhachHangDao();
        //    NhanVienDao nv = new NhanVienDao();

        //    ViewBag.MaKH = new SelectList(kh.ListKhachHang().Where(n => n.isdelete == false), "MaKH", "TenKH");
        //    ViewBag.MaNhanVien = new SelectList(nv.ListNhanVien().Where(n => n.isdelete == false), "MaNhanVien", "TenNhanVien");
        //}
        private void setViewbag(int? makh = null, int? manv = null)
        {

            KhachHangDao kh = new KhachHangDao();
            NhanVienDao nv = new NhanVienDao();

            ViewBag.MaKH = new SelectList(kh.ListKhachHang().Where(n => n.isdelete == false), "MaKH", "TenKH",makh);
            ViewBag.MaNhanVien = new SelectList(nv.ListNhanVien().Where(n => n.isdelete == false), "MaNhanVien", "TenNhanVien",manv);
        }
        
        
    }
}