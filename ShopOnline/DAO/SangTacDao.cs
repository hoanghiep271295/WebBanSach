using ShopOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.DAO
{
    public class SangTacDao
    {
        MyClassDbContent db;
        public SangTacDao()
        {
            db = new MyClassDbContent();
        }
        public IQueryable<SangTac> ListSangTac()
        {
            var res = (from s in db.SangTacs
                       where s.isdelete == false
                       orderby s.MaTG
                       select s);
            return res;
        }

        public bool checkSangTac(int MaSach, int MaTG)
        {
            var es = (from s in db.SangTacs
                      where s.isdelete == false && s.MaTG==MaTG
                      select s);
            
            foreach (var item in es)
            {
                if (item.MaSach == MaSach)
                    return true;

            }
            return false;
        }
        public int InsertSangTac(int Matg, int Masach,string tg)
        {
            SangTac st = new SangTac();
            st.MaTG = Matg;
            st.MaSach = Masach;
            st.TGSangTac = tg;
            st.isdelete = false;
            db.SangTacs.Add(st);
            db.SaveChanges();
            return st.MaTG;
        }

        public void UpdateSangTac(SangTac st)
        {
            SangTac stTmp = db.SangTacs.Find(st.MaTG,st.MaSach);
            if (stTmp != null)
            {
                //stTmp.MaSach = st.MaSach;
                //stTmp.MaTG = st.MaTG;
                stTmp.TGSangTac = st.TGSangTac;
                db.SaveChanges();
            }
        }

        public void DeleteSangTac(SangTac SangTacTmp)
        {
            SangTac st = db.SangTacs.Find(SangTacTmp.MaTG,SangTacTmp.MaSach);
            if (st != null)
            {
                db.SangTacs.Remove(st);
                db.SaveChanges();
            }

        }
        public SangTac FindSangTacByICode(string id)
        {
           String[] id_temp = id.Split(',');
            return db.SangTacs.Find(int.Parse(id_temp[0]), int.Parse(id_temp[1])); 
        }
    }
}