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
    public class SangTacController : BaseController
    {
        //
        // GET: /SangTac/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            SangTacDao ct = new SangTacDao();

            IQueryable<SangTac> lstSangTac = ct.ListSangTac();

            return View("List", lstSangTac.ToPagedList(pagecurrent, pagesize));
        }
        public ActionResult Add()
        {
            setViewbag();
            return View();
        }
        public ActionResult Delete(string id)
        {
            SangTacDao dao = new SangTacDao();

            SangTac st = dao.FindSangTacByICode(id);

            dao.DeleteSangTac(st);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<string> studentRecordDeletebyId)
        {
            SangTacDao dao = new SangTacDao();

            foreach (var id in studentRecordDeletebyId)
            {
                SangTac tg = dao.FindSangTacByICode(id);
                dao.DeleteSangTac(tg);
            }

            return RedirectToAction("List");
        }

        public ActionResult Edit(string id)
        {
            SangTacDao dao = new SangTacDao();

            SangTac st = dao.FindSangTacByICode(id);
            setViewbag(st.MaTG,st.MaSach);
            return View(st);
        }
        public ActionResult EditAction(SangTac st)
        {
            if (ModelState.IsValid)
            {
                SangTacDao dao = new SangTacDao();
                dao.UpdateSangTac(st);
                return RedirectToAction("List");
            }
            setViewbag();
            return View("Edit");
        }

        public ActionResult AddAction(SangTac st)
        {
            if (ModelState.IsValid)
            {
                SangTacDao dao = new SangTacDao();
                if (!dao.checkSangTac(st.MaSach,st.MaTG))
                {
                    dao.InsertSangTac(st.MaTG, st.MaSach, st.TGSangTac);
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.msg_err = "* Đã tồn tại !!!";
                    setViewbag();
                    return View("Add");
                }
                
            }
            setViewbag();
            return View("Add");
        }

        private void setViewbag(int? matg=null,int? masach = null)
        {

            TacGiaDao tg = new TacGiaDao();
            SachDao sach = new SachDao();

            ViewBag.MaTG = new SelectList(tg.ListTacGia().Where(n => n.isdelete == false), "MaTG", "TenTG", matg);
            ViewBag.MaSach = new SelectList(sach.ListSach().Where(n => n.isdelete == false), "MaSach", "TenSach",masach);
        }
    }
}