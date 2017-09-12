using ShopOnline.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.DAO;

namespace ShopOnline.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DanhMucValidationAttribute : ValidationAttribute
    {

        public static ValidationResult IsTenDanhMuc(string TenDanhMuc)
        {
            DanhMucDao dao = new DanhMucDao();
            var s = dao.checkTen(TenDanhMuc);
            if (s == true)
                return new ValidationResult("* Tên danh mục bị trùng !");
            else
                return ValidationResult.Success;

        }
    }
}//uk k biet