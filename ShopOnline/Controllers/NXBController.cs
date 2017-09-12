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
    public class NXBController : BaseController
    {
        //
        // GET: /NXB/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            NXBDao ct = new NXBDao();

            IQueryable<NXB> lstNXB = ct.ListNXB();

            return View("List", lstNXB.ToPagedList(pagecurrent, pagesize));
        }
        public ActionResult Add()
        {

            return View();
        }
        public ActionResult Delete(int id)
        {
            NXBDao dao = new NXBDao();

            NXB nxb = dao.FindNXBByICode(id);

            dao.DeleteNXB(nxb);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<int> studentRecordDeletebyId)
        {
            NXBDao dao = new NXBDao();

            foreach (var id in studentRecordDeletebyId)
            {
                NXB tg = dao.FindNXBByICode(id);
                dao.DeleteNXB(tg);
            }

            return RedirectToAction("List");
        }
        public ActionResult Edit(int id)
        {
            NXBDao dao = new NXBDao();

            NXB nxb = dao.FindNXBByICode(id);
            return View(nxb);
        }
        public ActionResult EditAction(NXB nxb)
        {
            if (ModelState.IsValid)
            {
                NXBDao dao = new NXBDao();
                dao.UpdateNXB(nxb);
                return RedirectToAction("List");
            }
            return View("Edit");

        }

        public ActionResult AddAction(NXB nxb)
        {
            if (ModelState.IsValid)
            {
                NXBDao dao = new NXBDao();
                if (!dao.checkTen(nxb.TenNXB))
                {
                    Session["Ok"] = "true";
                    dao.InsertNXB(nxb.MaNXB, nxb.TenNXB, nxb.SDT, nxb.DiaChi);
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.msg_err = "* Có tên nhà xuất bản này rồi !!!";
                    return View("Add",nxb);
                }
            }
            Session["Ok"] = "true";
            return View("Add", nxb);
        }
    }
}