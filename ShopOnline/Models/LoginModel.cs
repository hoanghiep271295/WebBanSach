using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "* Nhập tên đăng nhập !")]
        [StringLength(20)]
        public string TenDangNhap { get; set; }



        [Required(ErrorMessage = "* Nhập mật khẩu !")]
        [StringLength(20)]
        public string MatKhau { get; set; }

        public bool rememberme { get; set; }

    }
}