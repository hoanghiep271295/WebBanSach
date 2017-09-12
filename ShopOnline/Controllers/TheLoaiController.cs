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
    public class TheLoaiController : BaseController
    {
        //
        // GET: /TheLoai/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            TheLoaiSachDao ct = new TheLoaiSachDao();

            IQueryable<TheLoaiSach> lstTheLoaiSach = ct.ListTheLoai();

            return View("List", lstTheLoaiSach.ToPagedList(pagecurrent, pagesize));
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            TheLoaiSachDao dao = new TheLoaiSachDao();

            TheLoaiSach tg = dao.FindTheLoaiByICode(id);

            dao.DeleteTheLoai(tg);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<int> studentRecordDeletebyId)
        {
            TheLoaiSachDao dao = new TheLoaiSachDao();

            foreach (var id in studentRecordDeletebyId)
            {
                TheLoaiSach tg = dao.FindTheLoaiByICode(id);
                dao.DeleteTheLoai(tg);
            }

            return RedirectToAction("List");
        }  

        public ActionResult Edit(int id)
        {
            TheLoaiSachDao dao = new TheLoaiSachDao();

            TheLoaiSach tg = dao.FindTheLoaiByICode(id);
            return View(tg);
        }
        public ActionResult EditAction(TheLoaiSach tg)
        {
            if(ModelState.IsValid)
            {
                TheLoaiSachDao dao = new TheLoaiSachDao();
                dao.UpdateTheLoai(tg);
                return RedirectToAction("List");
            }
            return View("Edit");
        }

        public ActionResult AddAction(TheLoaiSach tg)
        {
            if(ModelState.IsValid)
            {
                Session["Ok"] = "true";
                TheLoaiSachDao dao = new TheLoaiSachDao();
                dao.InsertTheLoai(tg.TenLoaiSach);
                return RedirectToAction("List");
            }
            Session["Ok"] = "true";
            return View("Add");
        }
	}
}