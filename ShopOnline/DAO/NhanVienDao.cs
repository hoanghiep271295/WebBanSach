using ShopOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.DAO
{
    public class NhanVienDao
    {
        MyClassDbContent db;
        public NhanVienDao()
        {
            db = new MyClassDbContent();
        }
        public IQueryable<NhanVien> ListNhanVien()
        {
            var res = (from s in db.NhanViens
                       where s.isdelete == false
                       orderby s.MaNhanVien
                       select s);
            return res;
        }

        public bool checkTenDN(string TenDangNhap)
        {
            var es = (from s in db.NhanViens
                      where s.isdelete == false
                      select s);
            foreach (var item in es)
            {
                if (item.TenDangNhap == TenDangNhap)
                    return true;

            }
            return false;
        }

        public int InsertNhanVien( string Name,string sdt,string email, string tendn,string mk,bool? pq)
        {
            NhanVien nv = new NhanVien();
            nv.TenNhanVien = Name;
            nv.TenDangNhap = tendn;
            nv.MatKhau = mk;
            nv.DienThoai = sdt;
            nv.Email = email;
            nv.PhanQuyen = pq;
            nv.isdelete = false;
            db.NhanViens.Add(nv);
            db.SaveChanges();
            return nv.MaNhanVien;
        }

        public void UpdateNhanVien(NhanVien nv)
        {
            NhanVien nvTmp = db.NhanViens.Find(nv.MaNhanVien);
            if (nvTmp != null)
            {
                nvTmp.TenNhanVien = nv.TenNhanVien;
                nvTmp.TenDangNhap = nv.TenDangNhap;
                //nvTmp.MatKhau = nv.MatKhau;
                nvTmp.Email = nv.Email;
                nvTmp.DienThoai = nv.DienThoai;
                nvTmp.PhanQuyen = nv.PhanQuyen;
                db.SaveChanges();
            }
        }

        public void DeleteNhanVien(NhanVien NhanVienTmp)
        {
            NhanVien nv = db.NhanViens.Find(NhanVienTmp.MaNhanVien);
            if (nv != null)
            {
                db.NhanViens.Remove(nv);
                db.SaveChanges();
            }

        }
        public NhanVien FindNhanVienByICode(int id)
        {
            return db.NhanViens.Find(id);

        }

        
    }
}