using ShopOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.DAO
{
    public class TacGiaDao
    {
        MyClassDbContent db;
        public TacGiaDao()
        {
            db = new MyClassDbContent();
        }
         public IQueryable<TacGia> ListTacGia()
        {
            var res = (from s in db.TacGias
                       where s.isdelete == false
                       orderby s.MaTG
                       select s );
            return res;
        }

         public List<TacGia> listTacGia(int id)
         {
             if (id == 0)
             {
                 return db.TacGias.Where(n => n.isdelete == false).Take(5).ToList();
             }
             else
             {
                 return db.TacGias.Where(n => n.isdelete == false).ToList();
             }

         }
         public bool checkTen(string TenTG)
         {
             var es = (from s in db.TacGias
                       where s.isdelete == false
                       select s);
             foreach (var item in es)
             {
                 if (item.TenTG == TenTG)
                     return true;

             }
             return false;
         }
         public int InsertTacGia(string Name, string TTTG)
         {
             TacGia tg = new TacGia();
             tg.TenTG = Name;
             tg.ThongTinTG = TTTG;
             tg.isdelete = false;
             db.TacGias.Add(tg);
             db.SaveChanges();
             return tg.MaTG;
         }

         public void UpdateTacGia(TacGia tg)
         {
             TacGia tgTmp = db.TacGias.Find(tg.MaTG);
             if (tgTmp != null)
             {
                 tgTmp.TenTG = tg.TenTG;
                 tgTmp.ThongTinTG = tg.ThongTinTG;
                 db.SaveChanges();
             }
         }

         public void DeleteTacGia(TacGia tacgiaTmp)
         {
             TacGia tg = db.TacGias.Find(tacgiaTmp.MaTG);
             if (tg != null)
             {
                 db.TacGias.Remove(tg);
                 db.SaveChanges();
             }

         }
         public TacGia FindTacGiaByICode(int id)
         {
             return db.TacGias.Find(id);

         }

         public List<Sach> listSach_TacGia(int id)
         {
             var result = (from tg in db.TacGias
                           join st in db.SangTacs on tg.MaTG equals st.MaTG
                           join s in db.Saches on st.MaSach equals s.MaSach
                           where tg.MaTG == id
                           select s);
             return result.ToList();
         }
    }
}