using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Entities;

namespace ShopOnline.DAO
{
    public class KhachHangDao
    {
         MyClassDbContent db;
        public KhachHangDao()
        {
            db = new MyClassDbContent();
        }
        public bool Login(string TenDangNhap, string MatKhau)
        {

            var rs = db.KhachHangs.Count(x => x.TenDangNhap == TenDangNhap && x.MatKhau == MatKhau);
            if (rs > 0)
            {
                return true;
            }
            else
                return false;
        }

        public KhachHang GetKH(string TenDangNhap, string MatKhau)
        {

            var rs = (from s in db.KhachHangs
                       where s.isdelete == false && s.TenDangNhap==TenDangNhap && s.MatKhau==MatKhau
                       select s);
            return rs.FirstOrDefault();
        }

         public IQueryable<KhachHang> ListKhachHang()
        {
            var res = (from s in db.KhachHangs
                       where s.isdelete == false
                       orderby s.MaKH
                       select s);
            return res;
        }

         public bool checkTenDN(string TenDangNhap)
         {
             var es = (from s in db.KhachHangs
                       where s.isdelete == false
                       select s);
             foreach (var item in es)
             {
                 if (item.TenDangNhap == TenDangNhap)
                     return true;

             }
             return false;
         }
         public int InsertKhachHang(string Name,string email, string sdt,string mk,DateTime? NgaySinh,bool? GioiTinh,string dc,string tendn)
         {
             KhachHang kh = new KhachHang();
             kh.TenKH = Name;
             kh.GioiTinh = GioiTinh;
             kh.NgaySinh = NgaySinh;
             kh.SoDienThoai = sdt;
             kh.MatKhau = mk;
             kh.DiaChi = dc;
             kh.Email = email;
             kh.TenDangNhap = tendn;
             kh.isdelete = false;
             db.KhachHangs.Add(kh);
             db.SaveChanges();
             return kh.MaKH;
         }

         public void UpdateKhachHang(KhachHang kh)
         {
             KhachHang khTmp = db.KhachHangs.Find(kh.MaKH);
             if (khTmp != null)
             {
                 khTmp.TenKH = kh.TenKH;
                 khTmp.SoDienThoai = kh.SoDienThoai;
                 khTmp.MatKhau=kh.MatKhau;
                 khTmp.DiaChi=kh.DiaChi;
                 khTmp.NgaySinh = kh.NgaySinh;
                 khTmp.GioiTinh = kh.GioiTinh;
                 khTmp.TenDangNhap=kh.TenDangNhap;
                 khTmp.Email = kh.Email;
                 db.SaveChanges();
             }
         }

         public void DeleteKhachHang(KhachHang KhachHangTmp)
         {
             KhachHang kh = db.KhachHangs.Find(KhachHangTmp.MaKH);
             if (kh != null)
             {
                 db.KhachHangs.Remove(kh);
                 db.SaveChanges();
             }

         }
         public KhachHang FindKhachHangByICode(int id)
         {
             return db.KhachHangs.Find(id);

         }

        
    }
}