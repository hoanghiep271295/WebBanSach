using ShopOnline.DAO;
using ShopOnline.Entities;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Controllers
{
    public class KhachHangLoginController : Controller
    {
        //
        // GET: /KhachHangLogin/
        public ActionResult Login()
        {
            return View("LoginKH");
        }

        public ActionResult DangKyKH()
        {
            return View();
        }
        public ActionResult LoginAction(LoginModel kh)
        {
            if (ModelState.IsValid)
            {
                KhachHangDao khdao = new KhachHangDao();
                bool check = khdao.Login(kh.TenDangNhap,kh.MatKhau);

                if (check == true)
                {
                    Session["TenDangNhapKH"] = kh.TenDangNhap;
                    Session["MatKhauKH"] = kh.MatKhau;
                    KhachHang k = khdao.GetKH(kh.TenDangNhap,kh.MatKhau);
                    Session["TaiKhoanKH"] = k;
                    Session["IdKH"] = k.MaKH;
                    return RedirectToAction("Index", "TrangChu");
                }
                else
                {
                    ViewBag.Error = "Lỗi đăng nhập !";
                    return View("LoginKH");
                }

            }

            return View("LoginKH");
        }

        public ActionResult Logout()
        {
            Session["TenDangNhapKH"] = null;
            Session["MatKhauKH"] = null;
            Session["TaiKhoanKH"] = null;
            Session["IdKH"] = null;
            Session[GlobalModel.SESSION_CART] = null;
            return View("LoginKH");
        }
        //public ActionResult AddAction(KhachHang kh)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        KhachHangDao dao = new KhachHangDao();
        //        if (!dao.checkTenDN(kh.TenDangNhap))
        //        {
        //            Session["Ok"] = "true";
        //            dao.InsertKhachHang(kh.TenKH, kh.Email, kh.SoDienThoai, kh.MatKhau, kh.NgaySinh, kh.GioiTinh, kh.DiaChi, kh.TenDangNhap);
        //            ViewBag.Success = "* Tạo thành công !";
        //            return RedirectToAction("Index", "TrangChu");
        //        }
        //        else
        //        {
        //            ViewBag.msg_err = "* Có tên đăng nhập này rồi !!!";
        //            return View("DangKyKH");
        //        }


        //    }
        //    Session["Ok"] = "true";
        //    return View("DangKyKH");

        //}

        [HttpPost]
        public ActionResult DangKyKH(FormCollection f)
        {
            KhachHang kh = new KhachHang();
            KhachHangDao dao = new KhachHangDao();
            string TenKH = f["txtTenKH"].ToString();
            DateTime? NgaySinh = DateTime.Parse(f["txtNgaySinh"]);
            string SoDienThoai = f["txtSDT"].ToString();
            string DiaChi = f["txtDiaChi"].ToString();
            bool? GioiTinh=true;
            if (f["txtGioiTinh"].ToString() == "Nam")
            {
               GioiTinh = true;
            }
            if (f["txtGioiTinh"].ToString() == "Nữ")
            {
                GioiTinh = false;
            }
            string Email = f["txtEmail"].ToString();
            string TenDangNhap = f["txtTenDangNhap"].ToString();
            string MatKhau = f["txtMatKhau"].ToString();
            dao.InsertKhachHang(TenKH,Email,SoDienThoai,MatKhau,NgaySinh,GioiTinh,DiaChi,TenDangNhap);
            ViewBag.dangky = "Bạn đã đăng ký thành công !";
            return View("LoginKH");

        }

        public ActionResult ShowHoaDon(int idKH)
        {
            HoaDonDao dao = new HoaDonDao();
            var list_hoadon = dao.FindHoaDonByIdKhachHang(idKH);
            return View(list_hoadon);
        }
        public ActionResult ShowCTHD(int id)
        {
            ChiTietHoaDonDao dao = new ChiTietHoaDonDao();
            var list_cthoadon = dao.FindChiTietHoaDonById(id);
            return View(list_cthoadon);
        }

        public ActionResult ThongTinKH()
        {
            KhachHang k = (KhachHang)Session["TaiKhoanKH"];
            return View(k);
        }

        public ActionResult DoiMK()
        {
            return View("DoiMatKhau");
        }
        public ActionResult DoiMatKhau(FormCollection f)
        {
            KhachHang k = (KhachHang)Session["TaiKhoanKH"];
            KhachHangDao dao = new KhachHangDao();
            string mkcu = f["mkcu"].ToString();
            string mkmoi = f["MatKhau"].ToString();
            string nlmk = f["NhaplaimatKhau"].ToString();
            if (ModelState.IsValid)
            {
                if (mkcu == k.MatKhau.Trim())
                {
                    if (mkmoi.Trim() == nlmk.Trim())
                    {
                        k.MatKhau = mkmoi;
                        dao.UpdateKhachHang(k);
                        ViewBag.doimk = "Đổi mật khẩu thành công !";
                        return View("ThongTinKH", k);
                    }
                    else
                    {
                        ViewBag.loinhapmk = "Mật khẩu nhập lại không khớp !";
                        return View("DoiMatKhau");
                    }
                }
                else
                {
                    ViewBag.loidoimk = "Mật khẩu cũ của bạn không đúng !";
                    return View("DoiMatKhau");
                }
            }
            return View("DoiMatKhau");
        }
	}
}