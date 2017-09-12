using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Entities;
using ShopOnline.DAO;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        MyClassDbContent db=new MyClassDbContent();

        [HttpGet]
        public ActionResult Login()
        {
            LoginModel nv = checkCookie();
            if(nv==null)
            {
                return View();
            }
            else
            {
                return View(nv);
            }   
        }

        public ActionResult Logout()
        {
            Session["TenDangNhap"] = null;
            Session["MatKhau"] = null;
            if (Session["Rememberme"] == "true")
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View("Login");
            }
            //if (Request.Cookies["TenDangNhap"] != null)
            //{
            //    HttpCookie ckname2 = new HttpCookie("TenDangNhap");
            //    ckname2.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(ckname2);

            //}
            //if (Request.Cookies["MatKhau"] != null)
            //{
            //    HttpCookie ckmk2 = new HttpCookie("MatKhau");
            //    ckmk2.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(ckmk2);
            //}
            
        }
        public ActionResult Panelo()
        {
            return View();
        }

        public LoginModel checkCookie()
        {
            LoginModel nv = new LoginModel();
            string TenDangNhap = string.Empty;
            string MatKhau = string.Empty;
            if (Request.Cookies["TenDangNhap"] != null)
            {
                TenDangNhap = Request.Cookies["TenDangNhap"].Value;
                
            }
            if (Request.Cookies["MatKhau"] != null)
            {
                MatKhau = Request.Cookies["MatKhau"].Value;
               
            }
            if (!String.IsNullOrEmpty(TenDangNhap) && !String.IsNullOrEmpty(MatKhau))
            {

                nv.TenDangNhap = TenDangNhap;
                nv.MatKhau = MatKhau;

            }
            else
            {
                nv=null;
            }
            return nv;
        }

        [HttpPost]
       
        public ActionResult LoginAction(LoginModel nv)
        {
             if (ModelState.IsValid)
             {
                 AdminDao ad = new AdminDao();
                 bool check = ad.Login(nv.TenDangNhap, nv.MatKhau);

                 if (check == true)
                 {
                     Session["TenDangNhap"] = nv.TenDangNhap;
                     Session["MatKhau"] = nv.MatKhau;
                     
                     if(nv.rememberme)
                     {
                         HttpCookie ckname = new HttpCookie("TenDangNhap");
                         ckname.Expires = DateTime.Now.AddMinutes(5);
                         ckname.Value = nv.TenDangNhap;
                         Response.Cookies.Add(ckname);

                         HttpCookie ckmk = new HttpCookie("MatKhau");
                         ckmk.Expires = DateTime.Now.AddMinutes(5);
                         ckmk.Value = nv.MatKhau;
                         Response.Cookies.Add(ckmk);

                         Session["Rememberme"] = "true";
                     }
                     else
                     {
                         Session["Rememberme"] = "false";
                     }
                     return RedirectToAction("Panelo", "Admin");
                 }
                 else
                 {
                     ViewBag.Error = "Lỗi đăng nhập !";
                     return View("Login");
                 }
                     
             }

             return View("Login");
        }
        
	}
}