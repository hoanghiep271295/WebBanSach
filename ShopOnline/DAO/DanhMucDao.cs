using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Entities;
using System.Web.Mvc;

namespace ShopOnline.DAO
{
    public class DanhMucDao
    {
         MyClassDbContent db;
        public DanhMucDao()
        {
            db = new MyClassDbContent();
        }
         public IQueryable<DanhMuc> ListDanhMuc()
        {
            var res = (from s in db.DanhMucs
                       where s.isdelete == false
                       orderby s.MaDanhMuc
                       select s);
            return res;
        }

         public IQueryable<string> ListTenDanhMuc()
         {
             var res = (from s in db.DanhMucs
                        where s.isdelete == false
                        select s.TenDanhMuc);
             return res;
         }

         public string GetTenDanhMuc(int id)
         {
             var res = (from s in db.DanhMucs
                        where s.isdelete == false && s.MaDanhMuc==id
                        select s.TenDanhMuc);
             return res.FirstOrDefault();
         }
         public List<DanhMuc> listDanhMuc(int id)
         {
             if (id == 0)
             {
                 return db.DanhMucs.Where(n =>n.isdelete == false).Take(5).ToList();
             }
             else
             {
                 return db.DanhMucs.Where(n => n.isdelete == false).ToList();
             }

         }
        public bool checkTen(string TenDanhMuc)
         {
             var es = (from s in db.DanhMucs
                        where s.isdelete == false
                        select s);
            foreach (var item in es)
             {
                 if (item.TenDanhMuc == TenDanhMuc)
                     return true;
                 
             }
            return false;
         }
       
         public int InsertDanhMuc(string Name)
         {
             DanhMuc dm = new DanhMuc();
             dm.TenDanhMuc = Name;
             dm.isdelete = false;
             db.DanhMucs.Add(dm);
             db.SaveChanges();
             return dm.MaDanhMuc;
         }

         public void UpdateDanhMuc(DanhMuc dm)
         {
             DanhMuc dmTmp = db.DanhMucs.Find(dm.MaDanhMuc);
             if (dmTmp != null)
             {
                 dmTmp.TenDanhMuc = dm.TenDanhMuc;
                 db.SaveChanges();
             }
         }

         public void DeleteDanhMuc(DanhMuc DanhMucTmp)
         {
             DanhMuc dm = db.DanhMucs.Find(DanhMucTmp.MaDanhMuc);
             if (dm != null)
             {
                 db.DanhMucs.Remove(dm);
                 db.SaveChanges();
             }

         }
         public DanhMuc FindDanhMucByICode(int id)
         {
             return db.DanhMucs.Find(id);

         }

        //----------------------LAY SACH TU DANH MUC------------

        public List<Sach> listSach_DanhMuc(int id)
        {
            //var count_sach = db.Saches.Where(n => n.MaDanhMuc == id).Count();
            //if(count_sach > 0)
            //{
                return db.Saches.Where(n => n.MaDanhMuc == id && n.isdelete==false).ToList();
            //}
        }
    }
}