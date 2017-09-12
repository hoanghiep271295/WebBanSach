using ShopOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.DAO
{
    public class HoaDonDao
    {
        MyClassDbContent db;
        public HoaDonDao()
        {
            db = new MyClassDbContent();
        }
        public IQueryable<HoaDon> ListHoaDon()
        {
            var res = (from s in db.HoaDons
                       where s.isdelete == false
                       orderby s.MaHoaDon
                       select s);
            return res;
        }
        public int InsertHoaDon( DateTime? ThoiGian, int? MaKH,  int? MaNhanVien)
        {
            HoaDon hd = new HoaDon();
         
            hd.ThoiGian = ThoiGian;
            hd.MaKH = MaKH;
            hd.MaNhanVien = MaNhanVien;
            hd.isdelete = false;
            db.HoaDons.Add(hd);
            db.SaveChanges();
            return hd.MaHoaDon;
        }

        public void UpdateHoaDon(HoaDon hd)
        {
            HoaDon hdTmp = db.HoaDons.Find(hd.MaHoaDon);
            if (hdTmp != null)
            {
               
                hdTmp.ThoiGian = hd.ThoiGian;
                //hdTmp.MaKH = hd.MaKH;
               
                hdTmp.MaNhanVien = hd.MaNhanVien;
                db.SaveChanges();
            }
        }

        public void DeleteHoaDon(HoaDon HoaDonTmp)
        {
            HoaDon hd = db.HoaDons.Find(HoaDonTmp.MaHoaDon);
            if (hd != null)
            {
                db.HoaDons.Remove(hd);
                db.SaveChanges();
            }

        }
        public HoaDon FindHoaDonByICode(int id)
        {
            return db.HoaDons.Find(id);

        }

        public IQueryable<HoaDon> FindHoaDonByIdKhachHang(int id)
        {
            var hd=(from s in db.HoaDons 
                   where s.isdelete==false && s.MaKH==id
                   select s);
            return hd;
        }
    }
}