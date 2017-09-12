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
    public class TacGiaController : BaseController
    {
        //
        // GET: /TacGia/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            TacGiaDao ct = new TacGiaDao();

            IQueryable<TacGia> lstTacGia = ct.ListTacGia();

            return View("List", lstTacGia.ToPagedList(pagecurrent, pagesize));
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            TacGiaDao dao = new TacGiaDao();

            TacGia tg = dao.FindTacGiaByICode(id);

            dao.DeleteTacGia(tg);
            return RedirectToAction("List");
        }
       
        [HttpPost]
        public ActionResult deleteRow(IEnumerable<int> studentRecordDeletebyId)
        {
            TacGiaDao dao = new TacGiaDao();

            foreach (var id in studentRecordDeletebyId)
            {
                TacGia tg = dao.FindTacGiaByICode(id);
                dao.DeleteTacGia(tg);
            }
            
            return RedirectToAction("List");
        }  

        public ActionResult Edit(int id)
        {
            TacGiaDao dao = new TacGiaDao();

            TacGia tg = dao.FindTacGiaByICode(id);
            return View(tg);
        }
        public ActionResult EditAction(TacGia tg)
        {
            if(ModelState.IsValid)
            {
                TacGiaDao dao = new TacGiaDao();
                dao.UpdateTacGia(tg);
                return RedirectToAction("List");
            }
            return View("Edit");
        }

        public ActionResult AddAction(TacGia tg)
        {
            if(ModelState.IsValid)
            {
                TacGiaDao dao = new TacGiaDao();
                if (!dao.checkTen(tg.TenTG))
                {
                    Session["Ok"] = "true";
                    dao.InsertTacGia(tg.TenTG, tg.ThongTinTG);
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.msg_err = "* Tác giả đã tồn tại !!!";
                    return View("Add");
                }
            }
            Session["Ok"] = "true";
            return View("Add");
        }
	}
}