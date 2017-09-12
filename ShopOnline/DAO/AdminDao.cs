using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.Entities;

namespace ShopOnline.DAO
{
    
    public class AdminDao
    {
        MyClassDbContent db;
        public AdminDao()
        {
             db = new MyClassDbContent();
        }
        public bool Login(string Name,string Password)
        {
            
            var rs = db.NhanViens.Count(x => x.TenDangNhap == Name && x.MatKhau == Password );
            if (rs > 0)
            {
                return true;
            }
            else
                return false;
        }

      
    }
}