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
    public class SachController : BaseController
    {
        //
        // GET: /Sach/
        public ActionResult List(int? page)
        {
            int pagesize = 10;
            int pagecurrent = (page ?? 1); //
            SachDao ct = new SachDao();

            IQueryable<Sach> lstSach = ct.ListSach();

            return View("List", lstSach.ToPagedList(pagecurrent, pagesize));
        }

        

        public ActionResult Add()
        {
            setViewbag();
            return View();
        }
        public ActionResult Delete(int id)
        {
            SachDao dao = new SachDao();

            Sach book = dao.FindSachByICode(id);

            dao.DeleteSach(book);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult deleteRow(IEnumerable<int> studentRecordDeletebyId)
        {
            SachDao dao = new SachDao();

            foreach (var id in studentRecordDeletebyId)
            {
                Sach tg = dao.FindSachByICode(id);
                dao.DeleteSach(tg);
            }

            return RedirectToAction("List");
        }  
        public ActionResult Edit(int id)
        {
            SachDao dao = new SachDao();
            
            Sach book = dao.FindSachByICode(id);
            setViewbag(book.MaTheLoai,book.MaNXB,book.MaDanhMuc);
            return View(book);
        }
        public ActionResult EditAction(Sach book, HttpPostedFileBase Anh)
        {
            if(ModelState.IsValid)
            {
                if (Anh.ContentType != null && Anh.ContentLength > 0)
                {
                    string path = Server.MapPath("~/images/Imagesbook/" + Anh.FileName);
                    Anh.SaveAs(path);
                    SachDao dao = new SachDao();
                    Sach s = new Sach();
                    s.MaSach = book.MaSach;
                    s.TenSach = book.TenSach;
                    s.SoLuongCon = book.SoLuongCon;
                    s.MaTheLoai = book.MaTheLoai;
                    s.MaDanhMuc = book.MaDanhMuc;
                    s.MaNXB = book.MaNXB;
                    s.TinhTrangKM = book.TinhTrangKM;
                    s.DonGia = book.DonGia;
                    s.Anh = Anh.FileName;
                    s.Mota = book.Mota;

                    dao.UpdateSach(s);
                    return RedirectToAction("List");
                }
            }
            setViewbag();
            return View("Edit");
        }

        public ActionResult AddAction(Sach book, HttpPostedFileBase Anh)
        {
            if(ModelState.IsValid)
            {
                if(Anh.ContentType!=null && Anh.ContentLength>0)
                {
                    string path = Server.MapPath("~/images/Imagesbook/" + Anh.FileName);
                    Anh.SaveAs(path);
                    SachDao dao = new SachDao();
                    if (!dao.checkTen(book.TenSach))
                    {
                        Session["Ok"] = "true";
                        dao.InsertSach(book.TenSach, book.SoLuongCon, book.MaTheLoai, book.MaDanhMuc, book.MaNXB, book.TinhTrangKM, book.DonGia, Anh.FileName, book.Mota);

                        return RedirectToAction("List");
                    }
                    else
                    {
                        ViewBag.msg_err = "* Sách đã tồn tại !!!";
                        setViewbag();
                        return View("Add");
                    }
                }
               
            }
            Session["Ok"] = "true";
            setViewbag();
            return View("Add");
        }

        private void setViewbag(int? matl = null,int? manxb = null,int? madm=null)
        {

            TheLoaiSachDao theloai = new TheLoaiSachDao();
            DanhMucDao danhmuc = new DanhMucDao();
            NXBDao nxb = new NXBDao();

            ViewBag.MaTheLoai = new SelectList(theloai.ListTheLoai().Where(n => n.isdelete == false).OrderBy(n => n.TenLoaiSach), "MaTheLoai", "TenLoaiSach",matl);
            ViewBag.MaDanhMuc = new SelectList(danhmuc.ListDanhMuc().Where(n => n.isdelete == false).OrderBy(n => n.TenDanhMuc), "MaDanhMuc", "TenDanhMuc",madm);
            ViewBag.MaNXB = new SelectList(nxb.ListNXB().Where(n => n.isdelete == false).OrderBy(n => n.TenNXB), "MaNXB", "TenNXB",manxb);
        }
	}
}