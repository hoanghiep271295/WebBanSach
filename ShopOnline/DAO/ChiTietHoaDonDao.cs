using ShopOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.DAO
{
    public class ChiTietHoaDonDao
    {
        MyClassDbContent db;
        public ChiTietHoaDonDao()
        {
            db = new MyClassDbContent();
        }
        public IQueryable<ChiTietHoaĐon> ListChiTietHoaDon()
        {
            var res = (from s in db.ChiTietHoaĐon
                       where s.isdelete == false
                       orderby s.MaHoaĐon
                       select s);
            return res;//th
        }

        public bool checkMaSach(int MaSach, int MaHD)
        {
            var es = (from s in db.ChiTietHoaĐon
                      where s.isdelete == false && s.MaHoaĐon==MaHD
                      select s);
            //if (es!=null)
            //            return true;
            foreach (var item in es)
            {
                if (item.MaSach == MaSach)
                    return true;

            }
            return false;
        }

        public int InsertChiTietHoaDon(int MaHD, int MaSach, int? SoLuongBan)
        {
            ChiTietHoaĐon ct = new ChiTietHoaĐon();
            ct.MaHoaĐon = MaHD;
            ct.MaSach = MaSach;
            ct.SoLuongBan = SoLuongBan;
            ct.isdelete = false;
            db.ChiTietHoaĐon.Add(ct);
            db.SaveChanges();
            return ct.MaHoaĐon;
        }

        public void UpdateChiTietHoaDon(ChiTietHoaĐon ct)
        {
            ChiTietHoaĐon ctTmp = db.ChiTietHoaĐon.Find(ct.MaHoaĐon,ct.MaSach);
            if (ctTmp != null)
            {
                ctTmp.SoLuongBan = ct.SoLuongBan;
                db.SaveChanges();
            }
        }

        public void DeleteChiTietHoaDon(ChiTietHoaĐon ChiTietHoaDonTmp)
        {
            ChiTietHoaĐon ct = db.ChiTietHoaĐon.Find(ChiTietHoaDonTmp.MaHoaĐon,ChiTietHoaDonTmp.MaSach);
            if (ct != null)
            {
                db.ChiTietHoaĐon.Remove(ct);
                db.SaveChanges();
            }
        }
        public ChiTietHoaĐon FindChiTietHoaDonByICode(string id)
        {
            String[] id_temp = id.Split(',');
            return db.ChiTietHoaĐon.Find(int.Parse(id_temp[0]), int.Parse(id_temp[1])); 
        }

        public IQueryable<ChiTietHoaĐon> FindChiTietHoaDonById(int id)
        {
            var cthd = (from s in db.ChiTietHoaĐon where s.isdelete==false && s.MaHoaĐon==id select s);
            return cthd;
        }
    }
}