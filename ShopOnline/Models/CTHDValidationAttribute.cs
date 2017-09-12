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
    public class CTHDValidationAttribute : ValidationAttribute
    {
        public static ValidationResult IsMaSach(int MaSach,int MaHD)
        {
            ChiTietHoaDonDao dao = new ChiTietHoaDonDao();
            var s = dao.checkMaSach(MaSach,MaHD);
            if (s == true)
                return new ValidationResult("* Sách bị trùng !");
            else
                return ValidationResult.Success;

        }
    }
}