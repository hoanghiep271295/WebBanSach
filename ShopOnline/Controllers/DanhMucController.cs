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
    public class DanhMucController : BaseController
    {
        //
        // GET: /DanhMuc/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            DanhMucDao ct = new DanhMucDao();

            IQueryable<DanhMuc> lstDanhMuc = ct.ListDanhMuc();

            return View("List", lstDanhMuc.ToPagedList(pagecurrent, pagesize));
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            DanhMucDao dao = new DanhMucDao();

            DanhMuc dm = dao.FindDanhMucByICode(id);

            dao.DeleteDanhMuc(dm);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<int> studentRecordDeletebyId)
        {
            DanhMucDao dao = new DanhMucDao();

            foreach (var id in studentRecordDeletebyId)
            {
                DanhMuc tg = dao.FindDanhMucByICode(id);
                dao.DeleteDanhMuc(tg);
            }

            return RedirectToAction("List");
        }  
        public ActionResult Edit(int id)
        {
            DanhMucDao dao = new DanhMucDao();

            DanhMuc dm = dao.FindDanhMucByICode(id);
            return View(dm);
        }
        public ActionResult EditAction(DanhMuc dm)
        {
            if(ModelState.IsValid)
            {
                DanhMucDao dao = new DanhMucDao();
                dao.UpdateDanhMuc(dm);
                return RedirectToAction("List");
            }
            return View("Edit");
        }

        public ActionResult AddAction(DanhMuc dm)
        {
            if(ModelState.IsValid)
            {
                DanhMucDao dao = new DanhMucDao();
                dao.InsertDanhMuc(dm.TenDanhMuc);
                return RedirectToAction("List");
            }
            return View("Add");
        }
	}
}