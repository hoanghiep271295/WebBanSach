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
    public class TrangChuController : Controller
    {
        //
        // GET: /Test/
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult SachGiamGiaTrangChuPartialView()
        {
            SachDao sd = new SachDao();
            List<Sach> lst_sach_gg = sd.listSachGiamGia(0);

            return PartialView(lst_sach_gg);
        }

        public PartialViewResult SachBanChayTrangChuPartialView()
        {
            SachDao sd = new SachDao();
            List<Sach> lst_sach_bc = sd.listSachBanChay(0);
            return PartialView(lst_sach_bc);
        }

        public PartialViewResult SachMoiTrangChuPartialView()
        {
            SachDao sd = new SachDao();
            List<Sach> lst_sach_sm = sd.listSachMoi(0);
            return PartialView(lst_sach_sm);
        }

        public ActionResult IndexSachMoi()
        {
            SachDao sd = new SachDao();
            List<Sach> lst_sach_sm = sd.listSachMoi(1);
            return View(lst_sach_sm);
        }

        public ActionResult IndexSachGiamGia()
        {
            SachDao sd = new SachDao();
            List<Sach> lst_sach_sm = sd.listSachGiamGia(1);
            return View(lst_sach_sm);
        }

        public ActionResult IndexSachBanChay()
        {
            SachDao sd = new SachDao();
            List<Sach> lst_sach_sm = sd.listSachBanChay(1);
            return View(lst_sach_sm);
        }

        public ActionResult IndexChiTietSach(int id)
        {
            SachDao sd = new SachDao();
            //int totalRecord=0;
            var lst_sach_sm = sd.ListSachById(id);//
            return View(lst_sach_sm);
        }


        //---------------Danh muc-------------------

        public PartialViewResult DanhMucTrangChuPartialView()
        {
            DanhMucDao sd = new DanhMucDao();
            List<DanhMuc> lst_dm = sd.listDanhMuc(0);
            return PartialView(lst_dm);
        }
        public PartialViewResult DanhMucTrangChufullPartialView()
        {
            DanhMucDao sd = new DanhMucDao();
            List<DanhMuc> lst_dm = sd.listDanhMuc(1);
            return PartialView(lst_dm);
        }
        public ActionResult IndexSach_DanhMuc(int id)
        {
            DanhMucDao sd = new DanhMucDao();
            var lst_sach_dm = sd.listSach_DanhMuc(id) ;
            ViewBag.TenDanhMuc = sd.GetTenDanhMuc(id);
            return View(lst_sach_dm);
        }

        

        

        //---------------The loai------------------------

        public PartialViewResult TheLoaiTrangChuPartialView()
        {
            TheLoaiSachDao sd = new TheLoaiSachDao();
            List<TheLoaiSach> lst_tl = sd.listTheLoai(0);
            return PartialView(lst_tl);
        }

        public PartialViewResult TheLoaiTrangChufullPartialView()
        {
            TheLoaiSachDao sd = new TheLoaiSachDao();
            List<TheLoaiSach> lst_tl = sd.listTheLoai(1);
            return PartialView(lst_tl);
        }

        public ActionResult IndexSach_TheLoai(int id)
        {
            TheLoaiSachDao tld = new TheLoaiSachDao();
            var lst_sach_tl = tld.listSach_TheLoai(id);
            ViewBag.TheLoai = tld.FindTheLoaiByICode(id).TenLoaiSach;
            return View(lst_sach_tl);
        }

        //--------------------Tac Gia-----------------

        public PartialViewResult TacGiaTrangChuPartialView()
        {
            TacGiaDao sd = new TacGiaDao();
            List<TacGia> lst_tg = sd.listTacGia(0);
            return PartialView(lst_tg);
        }

        public PartialViewResult TacGiaTrangChufullPartialView()
        {
            TacGiaDao sd = new TacGiaDao();
            List<TacGia> lst_tg = sd.listTacGia(1);
            return PartialView(lst_tg);
        }

        public ActionResult IndexSach_TacGia(int id)
        {
            TacGiaDao tgd = new TacGiaDao();
            var lst_sach_tg = tgd.listSach_TacGia(id);
            ViewBag.TacGia = tgd.FindTacGiaByICode(id).TenTG;
            return View(lst_sach_tg);
        }

        //------------------NXB-----------------------

        public PartialViewResult NXBTrangChuPartialView()
        {
            NXBDao sd = new NXBDao();
            List<NXB> lst_nxb = sd.listNXB(0);
            return PartialView(lst_nxb);
        }

        public PartialViewResult NXBTrangChufullPartialView()
        {
            NXBDao sd = new NXBDao();
            List<NXB> lst_nxb = sd.listNXB(1);
            return PartialView(lst_nxb);
        }

        public ActionResult IndexSach_NXB(int id)
        {
            NXBDao nxbd = new NXBDao();
            var lst_sach_nxb = nxbd.listSach_NXB(id);
            ViewBag.TenNXB = nxbd.FindNXBByICode(id).TenNXB;
            return View(lst_sach_nxb);
        }

        //------------------Tim kiem-----------------------
        [HttpPost]
        public ActionResult TimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtTimKiem"].ToString();
            SachDao dao=new SachDao();
            List<Sach> listKQTK = dao.TimSach(sTuKhoa);
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if (listKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sách nào!!!";
                return View();
            }
            return View("TimKiem",listKQTK.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LienHe()
        {
            return View();
        }
        public ActionResult HuongDan()
        {
            return View();
        }
        public ActionResult GioiThieu()
        {
            return View();
        }
	}
}