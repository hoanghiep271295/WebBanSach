using ShopOnline.Entities;
using ShopOnline.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.DAO
{
    public class SachDao
    {
        MyClassDbContent db;
        public SachDao()
        {
            db = new MyClassDbContent();
        }
        public IQueryable<Sach> ListSach()
        {
            var res = (from s in db.Saches
                       where s.isdelete ==false
                       orderby s.MaSach descending 
                       select s);
            return res;
        }

        public bool checkMaSach(int MaSach)
        {
            var es = (from s in db.Saches
                      where s.isdelete == false
                      select s);
            foreach (var item in es)
            {
                if (item.MaSach == MaSach)
                    return true;

            }
            return false;
        }

       
        public bool checkTen(string TenSach)
        {
            var es = (from s in db.Saches
                      where s.isdelete == false
                      select s);
            foreach (var item in es)
            {
                if (item.TenSach == TenSach)
                    return true;

            }
            return false;
        }
        
        public int InsertSach( string Name, int? soluong, int? Matl, int? Madm, int? Manxb, int? TTKM, decimal? dongia,string anh,string mota) 
        {
            Sach book = new Sach();
            book.TenSach = Name;
            book.SoLuongCon = soluong;
            book.MaTheLoai = Matl;
            book.MaDanhMuc = Madm;
            book.MaNXB = Manxb;
            book.TinhTrangKM = TTKM;
            book.DonGia = dongia;
            book.Anh = anh;
            book.Mota = mota;
            book.isdelete = false;
            db.Saches.Add(book);
            db.SaveChanges();
            return book.MaSach;
        }

        public void UpdateSach(Sach book)
        {
            Sach bookTmp = db.Saches.Find(book.MaSach);
            if (bookTmp != null)
            {
                bookTmp.TenSach = book.TenSach;
                bookTmp.SoLuongCon = book.SoLuongCon;
                bookTmp.MaTheLoai = book.MaTheLoai;
                bookTmp.Anh = book.Anh;
                bookTmp.MaDanhMuc = book.MaDanhMuc;
                bookTmp.MaNXB = book.MaNXB;
                bookTmp.TinhTrangKM = book.TinhTrangKM;
                bookTmp.DonGia = book.DonGia;
                bookTmp.Mota = book.Mota;
                db.SaveChanges();
            }
        }

        public void DeleteSach(Sach SachTmp)
        {
            Sach book = db.Saches.Find(SachTmp.MaSach);
            if (book != null)
            {
                db.Saches.Remove(book);
                db.SaveChanges();
            }

        }
        public Sach FindSachByICode(int id)
        {
            return db.Saches.Find(id);

        }


        //-------get book-----------------
        public List<Sach> listSachGiamGia(int id)
        {
            if (id == 0)
            {
                return db.Saches.Where(n => n.MaDanhMuc == 3 && n.isdelete == false)
                    .OrderByDescending(n => n.MaSach).Take(4).ToList();
            }
            else
            {
                return db.Saches.Where(n => n.MaDanhMuc == 3 && n.isdelete == false)
                    .OrderByDescending(n => n.MaSach).ToList();
            }

        }

        public List<Sach> listSachMoi(int id)
        {
            if (id == 0)
            {
                return db.Saches.Where(n => n.MaDanhMuc == 2 && n.isdelete == false && n.SoLuongCon > 0)
                    .OrderByDescending(n => n.MaSach).Take(8).ToList();
            }
            else
            {
                return db.Saches.Where(n => n.MaDanhMuc == 2 && n.isdelete == false && n.SoLuongCon > 0)
                    .OrderByDescending(n => n.MaSach).ToList();
            }

        }

        public List<Sach> listSachBanChay(int id)
        {
            if (id == 0)
            {
                return db.Saches.Where(n => n.MaDanhMuc == 1 && n.isdelete == false && n.SoLuongCon > 0)
                    .OrderByDescending(n=>n.MaSach).Take(4).ToList();
            }
            else
            {
                return db.Saches.Where(n => n.MaDanhMuc == 1 && n.isdelete == false && n.SoLuongCon > 0)
                    .OrderByDescending(n => n.MaSach).ToList();
            }

        }

        //----------------------Chi tiet sach------------

        public SachViewModel ListSachById(int id)
        {
             
            var model = from a in db.Saches
                         join b in db.SangTacs on a.MaSach equals b.MaSach
                         join c in db.TacGias on b.MaTG equals c.MaTG
                         join d in db.NXBs on a.MaNXB equals d.MaNXB
                         where a.MaSach == id
                         select new SachViewModel
                         {
                             MaSach = a.MaSach,
                             TenSach=a.TenSach,
                             MaNXB=a.MaNXB,
                             TenNXB=d.TenNXB,
                             MaTG=b.MaTG,
                             TenTG=c.TenTG,
                             TGSangTac=b.TGSangTac,
                             DonGia=a.DonGia,
                             SoLuongCon=a.SoLuongCon,
                             TinhTrangKM=a.TinhTrangKM,
                             Anh=a.Anh,
                             Mota=a.Mota
                         };
             return model.FirstOrDefault();
        }
        //-------------tim kiem------------

        public List<Sach> TimSach(string key)
        {
            var lst = db.Saches.Where(n => n.TenSach.Contains(key)&& n.isdelete==false).ToList();
            return lst;
        }

    }
}