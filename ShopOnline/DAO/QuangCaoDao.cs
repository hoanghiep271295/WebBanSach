using ShopOnline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.DAO
{
    public class QuangCaoDao
    {
        MyClassDbContent db;
        public QuangCaoDao()
        {
            db = new MyClassDbContent();
        }
        public IQueryable<QuangCao> ListQuangCao()
        {
            var res = (from s in db.QuangCaos
                       where s.isdelete == false
                       orderby s.MaQC
                       select s);
            return res;
        }

        public bool checkTenurl(string Tenurl)
        {
            var es = (from s in db.QuangCaos
                      where s.isdelete == false
                      select s);
            foreach (var item in es)
            {
                if (item.TenURL == Tenurl)
                    return true;

            }
            return false;
        }
        public int InsertQuangCao(string Name, string url, string NoiDung, DateTime? NgayDang)
        {
            QuangCao qc = new QuangCao();
            qc.TenQC = Name;
            qc.TenURL = url;
            qc.NoiDung = NoiDung;
            qc.NgayDang = NgayDang;
            qc.isdelete = false;
            db.QuangCaos.Add(qc);
            db.SaveChanges();
            return qc.MaQC;
        }

        public void UpdateQuangCao(QuangCao qc)
        {
            QuangCao qcTmp = db.QuangCaos.Find(qc.MaQC);
            if (qcTmp != null)
            {
                qcTmp.TenQC = qc.TenQC;
                qcTmp.TenURL = qc.TenURL;
                qcTmp.NoiDung = qc.NoiDung;
                qcTmp.NgayDang = qc.NgayDang;
                db.SaveChanges();
            }
        }

        public void DeleteQuangCao(QuangCao QuangCaoTmp)
        {
            QuangCao qc = db.QuangCaos.Find(QuangCaoTmp.MaQC);
            if (qc != null)
            {
                db.QuangCaos.Remove(qc);
                db.SaveChanges();
            }

        }
        public QuangCao FindQuangCaoByICode(int id)
        {
            return db.QuangCaos.Find(id);

        }
    }
}