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
    public class ChiTietHoaDonController : BaseController
    {
        //
        // GET: /ChiTietHoaDon/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            ChiTietHoaDonDao ct = new ChiTietHoaDonDao();
            
            IQueryable<ChiTietHoaĐon> lstChiTietHoaDon = ct.ListChiTietHoaDon();
            
            return View("List", lstChiTietHoaDon.ToPagedList(pagecurrent,pagesize));
        }
        public ActionResult Add()
        {
            setViewbag();
            return View();
        }
        public ActionResult Delete(string id)
        {
            ChiTietHoaDonDao dao = new ChiTietHoaDonDao();

            ChiTietHoaĐon ct = dao.FindChiTietHoaDonByICode(id);

            dao.DeleteChiTietHoaDon(ct);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<string> studentRecordDeletebyId)
        {
            ChiTietHoaDonDao dao = new ChiTietHoaDonDao();

            foreach (var id in studentRecordDeletebyId)
            {
                ChiTietHoaĐon ct = dao.FindChiTietHoaDonByICode(id);
                dao.DeleteChiTietHoaDon(ct);
            }

            return RedirectToAction("List");
        }  
        public ActionResult Edit(string id)
        {
            ChiTietHoaDonDao dao = new ChiTietHoaDonDao();

            ChiTietHoaĐon ct = dao.FindChiTietHoaDonByICode(id);
            return View(ct);
        }
        public ActionResult EditAction(ChiTietHoaĐon ct)
        {
            if(ModelState.IsValid)
            {
                ChiTietHoaDonDao dao = new ChiTietHoaDonDao();
                dao.UpdateChiTietHoaDon(ct);
                return RedirectToAction("List");
            }
            return View("Edit");
        }

        public ActionResult AddAction(ChiTietHoaĐon ct)
        {
            if(ModelState.IsValid)
            {
               
                ChiTietHoaDonDao dao = new ChiTietHoaDonDao();
                 if(!dao.checkMaSach(ct.MaSach,ct.MaHoaĐon))
                 {
                     dao.InsertChiTietHoaDon(ct.MaHoaĐon, ct.MaSach, ct.SoLuongBan);
                     return RedirectToAction("List");
                 }
                 else
                 {
                     ViewBag.msg_err = "* Đã tồn tại chi tiết hóa đơn này !!!";
                     setViewbag();
                     return View("Add");
                 }
                
            }
            setViewbag();
            return View("Add");
        }

        private void setViewbag()
        {

            SachDao sd = new SachDao();
            HoaDonDao hd = new HoaDonDao();

            ViewBag.MaHoaĐon = new SelectList(hd.ListHoaDon().Where(n => n.isdelete == false).OrderBy(n => n.MaHoaDon), "MaHoaDon", "MaHoaDon");

            ViewBag.MaSach = new SelectList(sd.ListSach().Where(n => n.isdelete == false).OrderBy(n => n.TenSach), "MaSach", "TenSach");

        }
    }
}