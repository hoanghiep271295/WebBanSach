using ShopOnline.DAO;
using ShopOnline.Entities;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;
using System.Web.Script.Serialization;

namespace ShopOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShoppingCart/
        

        public ActionResult IndexShoppingCart()
        {
            var cartlist = Session[GlobalModel.SESSION_CART];

            var itemlist = new List<CartItem>();
            if (cartlist != null)
            {
                itemlist = (List<CartItem>)cartlist;
            }
            return View(itemlist);
        }
        public ActionResult ActionFinish()
        {
            
            return View();
        }

        public ActionResult AddItemSach(int idsach, int soluong,string url)
        {
            var cartlist = Session[GlobalModel.SESSION_CART];//lấy ra danh sách sách đang có trong sesion cart 
            Sach sach = (new SachDao()).FindSachByICode(idsach);

            if (cartlist != null)//nếu cart đã tồn tại
            {
                var itemlist = (List<CartItem>)cartlist;
                if(itemlist.Exists(n=>n.Sachs.MaSach==idsach))//nếu trong cart đã có sách thì chỉ cần tăng thêm số lượng
                {
                    foreach (var item in itemlist)
                    {
                        if (item.Sachs.MaSach == idsach)
                        {
                            item.Soluong += soluong;
                        }
                    }
                }
                else//nếu chưa có thì tiến hành tạo mới đối tượng và thêm vào cart 
                {
                    CartItem cartItem = new CartItem();
                    cartItem.Sachs = sach;
                    cartItem.Soluong = soluong;

                    itemlist.Add(cartItem);
                }

                Session[GlobalModel.SESSION_CART] = itemlist;
            }
            else //nếu chưa có thì tiến hành tạo mới đối tượng và thêm vào cart 
            {
                CartItem cartItem = new CartItem();
                cartItem.Sachs = sach;
                cartItem.Soluong = soluong;
                var itemlist = new List<CartItem>();

                itemlist.Add(cartItem);
                Session[GlobalModel.SESSION_CART]=itemlist;//cập nhật lại cart 
            }

            //return RedirectToAction("IndexShoppingCart");
            return Redirect(url);

        }

        
        public JsonResult Delete(int idsach)//id lấy ở ajax truyền lên
        {
            var lst_cart_session = (List<CartItem>)Session[GlobalModel.SESSION_CART];
            lst_cart_session.RemoveAll(n=>n.Sachs.MaSach==idsach);//tìm ra những sách trong cart có mã sách = id cần xóa để xóa nó đi
            return Json(new { 
             status = true 
            });
        }

        public JsonResult DeleteAll()
        {
            Session[GlobalModel.SESSION_CART] = null;
            
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(String model)
        {
            var cartlist_json = new JavaScriptSerializer().Deserialize<List<CartItem>>(model);
            var cartlist_session = (List<CartItem>)Session[GlobalModel.SESSION_CART];
            if (cartlist_session != null)
            {
                foreach(var item in cartlist_session)
                {
                    var cart_json = cartlist_json.SingleOrDefault(n=>n.Sachs.MaSach==item.Sachs.MaSach);
                    if (cart_json != null)
                    {
                        item.Soluong = cart_json.Soluong;
                    }
                }
                Session[GlobalModel.SESSION_CART] = cartlist_session;//cập nhật lại cart 
            }
            return Json(new
            {
                status = true
            });
        }

        public ActionResult CheckDonHang()
        {
            if (Session["TaiKhoanKH"] == null)
            {
                var cartlist = Session[GlobalModel.SESSION_CART];

                var itemlist = new List<CartItem>();
                if (cartlist != null)
                {
                    itemlist = (List<CartItem>)cartlist;
                }
                ViewBag.Error_msg = "Bạn phải đăng nhập trước khi mua hàng";
                return View("IndexShoppingCart", itemlist);
            }
            else
            {
                KhachHang kh = (KhachHang)Session["TaiKhoanKH"];
                return View(kh);
            }
                
           
        }
        public ActionResult ThanhToan()
        {
            

            try
            {
                var cartlist = Session[GlobalModel.SESSION_CART];

                HoaDonDao hddao = new HoaDonDao();

                KhachHang kh = (KhachHang)Session["TaiKhoanKH"];
                HoaDon hd = new HoaDon();
                hd.MaKH = kh.MaKH;
                hd.ThoiGian = DateTime.Now;
                hd.MaNhanVien = null;
                int s = hddao.InsertHoaDon(hd.ThoiGian, hd.MaKH, hd.MaNhanVien);

                if (cartlist != null)//nếu cart đã tồn tại
                {
                    var itemlist = (List<CartItem>)cartlist;

                    foreach (var item in itemlist)
                    {
                        ChiTietHoaDonDao ctdao = new ChiTietHoaDonDao();
                        ctdao.InsertChiTietHoaDon(s, item.Sachs.MaSach, item.Soluong);
                    }
                }
                Session[GlobalModel.SESSION_CART] = null;
            }
            catch
            {
                var cartlist = Session[GlobalModel.SESSION_CART];

                var itemlist = new List<CartItem>();
                if (cartlist != null)
                {
                    itemlist = (List<CartItem>)cartlist;
                }
                ViewBag.Error_msg = "Rất tiếc !!! Mua hàng thất bại !!!";
                return View("IndexShoppingCart", itemlist);
            }



            return RedirectToAction("ActionFinish");
        }

        public ActionResult ShoppingCartPartial()
        {
            int tongtien = 0;
            int TongSoluong = 0;
            var cartlist = Session[GlobalModel.SESSION_CART];
            if (cartlist != null )//nếu cart đã tồn tại
            {
                var itemlist = (List<CartItem>)cartlist;
                TongSoluong=itemlist.Count;
                if(TongSoluong>0)
                {
                    foreach (var item in itemlist)
                    {
                        int dongia_temp = (int)item.Sachs.DonGia;
                        int khuyenmai_temp = (int)item.Sachs.TinhTrangKM;
                        int thanhtien = (dongia_temp - dongia_temp * khuyenmai_temp / 100) * item.Soluong;
                        tongtien += thanhtien;
                    }
                   
                }
               

            }

            ViewBag.TongTien = tongtien;
            ViewBag.TongSoluong = TongSoluong;
            return PartialView();

        }

	}
}