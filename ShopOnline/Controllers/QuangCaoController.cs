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
    public class QuangCaoController : BaseController
    {
        //
        // GET: /QuangCao/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            QuangCaoDao ct = new QuangCaoDao();

            IQueryable<QuangCao> lstQuangCao = ct.ListQuangCao();

            return View("List", lstQuangCao.ToPagedList(pagecurrent, pagesize));
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            QuangCaoDao dao = new QuangCaoDao();

            QuangCao qc = dao.FindQuangCaoByICode(id);

            dao.DeleteQuangCao(qc);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<int> studentRecordDeletebyId)
        {
            QuangCaoDao dao = new QuangCaoDao();

            foreach (var id in studentRecordDeletebyId)
            {
                QuangCao qc = dao.FindQuangCaoByICode(id);
                dao.DeleteQuangCao(qc);
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(int id)
        {
            QuangCaoDao dao = new QuangCaoDao();

            QuangCao qc = dao.FindQuangCaoByICode(id);
            return View(qc);
        }
        public ActionResult EditAction(QuangCao qc)
        {
            if(ModelState.IsValid)
            {
                QuangCaoDao dao = new QuangCaoDao();
                dao.UpdateQuangCao(qc);
                return RedirectToAction("List");
            }
            return View("Edit");
        }

        public ActionResult AddAction(QuangCao qc)
        {
            if(ModelState.IsValid)
            {
                QuangCaoDao dao = new QuangCaoDao();
                if(!dao.checkTenurl(qc.TenURL))
                {
                    Session["Ok"] = "true";
                    dao.InsertQuangCao(qc.TenQC, qc.TenURL, qc.NoiDung, qc.NgayDang);
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.msg_err = "* Đã tồn tại đường dẫn url này !!!";
                    return View("Add");
                }
            }
            Session["Ok"] = "true";
            return View("Add");
        }
    }
}