using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Entities;

namespace ShopOnline.DAO
{
    public class TheLoaiSachDao
    {
        MyClassDbContent db;
        public TheLoaiSachDao()
        {
            db = new MyClassDbContent();
        }

        public IQueryable<TheLoaiSach> ListTheLoai()
        {
            var res = (from s in db.TheLoaiSaches
                       where s.isdelete == false 

                       orderby s.MaTheLoai
                       select s);
            return res;
        }

        public List<TheLoaiSach> listTheLoai(int id)
        {
            if (id == 0)
            {
                return db.TheLoaiSaches.Where(n => n.isdelete == false).Take(5).ToList();
            }
            else
            {
                return db.TheLoaiSaches.Where(n => n.isdelete == false).ToList();
            }

        }

        public bool checkTen(string TenTheLoai)
        {
            var es = (from s in db.TheLoaiSaches
                      where s.isdelete == false
                      select s);
            foreach (var item in es)
            {
                if (item.TenLoaiSach == TenTheLoai)
                    return true;

            }
            return false;
        }
       
        public int InsertTheLoai(string Name)
          {
            TheLoaiSach tl = new TheLoaiSach();
            tl.TenLoaiSach = Name;
            tl.isdelete = false;
            db.TheLoaiSaches.Add(tl);
            db.SaveChanges();
            return tl.MaTheLoai;
        }

        public void UpdateTheLoai(TheLoaiSach tl)
        {
            TheLoaiSach tlTmp = db.TheLoaiSaches.Find(tl.MaTheLoai);
            if (tlTmp != null)
            {
                tlTmp.TenLoaiSach = tl.TenLoaiSach;
                db.SaveChanges();
            }
        }

        public void DeleteTheLoai(TheLoaiSach theloaiTmp)
        {
            TheLoaiSach tl = db.TheLoaiSaches.Find(theloaiTmp.MaTheLoai);
            if (tl != null)
            {
                db.TheLoaiSaches.Remove(tl);
                db.SaveChanges();
            }

        }
        public TheLoaiSach FindTheLoaiByICode(int id)
        {
            return db.TheLoaiSaches.Find(id);

        }

        public List<Sach> listSach_TheLoai(int id)
        {
            //var count_sach = db.Saches.Where(n => n.MaDanhMuc == id).Count();
            //if(count_sach > 0)
            //{
            return db.Saches.Where(n => n.MaTheLoai == id && n.isdelete == false).ToList();
            //}
        }
    }
}