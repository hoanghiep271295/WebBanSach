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
    public class TheLoaiValidationAttribute : ValidationAttribute
    {

        public static ValidationResult IsTenTheLoai(string TenTheLoai)
        {
            TheLoaiSachDao dao = new TheLoaiSachDao();
            var s = dao.checkTen(TenTheLoai);
            if (s == true)
                return new ValidationResult("* Tên thể loại bị trùng !");
            else
                return ValidationResult.Success;

        }
    }
}