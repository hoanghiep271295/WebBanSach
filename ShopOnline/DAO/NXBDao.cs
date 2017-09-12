using ShopOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.DAO
{
    public class NXBDao
    {
         MyClassDbContent db;
        public NXBDao()
        {
            db = new MyClassDbContent();
        }
         public IQueryable<NXB> ListNXB()
        {
            var res = (from s in db.NXBs
                       where s.isdelete == false
                       orderby s.MaNXB
                       select s);
            return res;
        }

         public List<NXB> listNXB(int id)
         {
             if (id == 0)
             {
                 return db.NXBs.Where(n => n.isdelete == false).Take(5).ToList();
             }
             else
             {
                 return db.NXBs.Where(n => n.isdelete == false).ToList();
             }

         }
         public bool checkTen(string TenNXB)
         {
             var es = (from s in db.NXBs
                       where s.isdelete == false
                       select s);
             foreach (var item in es)
             {
                 if (item.TenNXB == TenNXB)
                     return true;

             }
             return false;
         }
         public int InsertNXB(int Manxb, string Name, string sdt ,string dc)
         {
             NXB nxb = new NXB();
             nxb.TenNXB = Name;
             nxb.MaNXB = Manxb;
             nxb.DiaChi = dc;
             nxb.SDT = sdt;
             nxb.isdelete = false;
             db.NXBs.Add(nxb);
             db.SaveChanges();
             return nxb.MaNXB;
         }

         public void UpdateNXB(NXB nxb)
         {
             NXB nxbTmp = db.NXBs.Find(nxb.MaNXB);
             if (nxbTmp != null)
             {
                 nxbTmp.TenNXB = nxb.TenNXB;
                 nxbTmp.SDT = nxb.SDT;
                 nxbTmp.DiaChi = nxb.DiaChi;
                 db.SaveChanges();
             }
         }

         public void DeleteNXB(NXB NXBTmp)
         {
             NXB nxb = db.NXBs.Find(NXBTmp.MaNXB);
             if (nxb != null)
             {
                 db.NXBs.Remove(nxb);
                 db.SaveChanges();
             }

         }
         public NXB FindNXBByICode(int id)
         {
             return db.NXBs.Find(id);

         }

         public List<Sach> listSach_NXB(int id)
         {
             //var count_sach = db.Saches.Where(n => n.MaDanhMuc == id).Count();
             //if(count_sach > 0)
             //{
             return db.Saches.Where(n => n.MaNXB == id && n.isdelete == false).ToList();
             //}
         }
    }
}