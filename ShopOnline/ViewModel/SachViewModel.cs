using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.ViewModel
{
    public class SachViewModel
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }

        //public int? MaTheLoai { get; set; }

        public int? MaNXB { get; set; }
        public string TenNXB { get; set; }
        public int MaTG { get; set; }
        public string TenTG { get; set; }
        public string TGSangTac { get; set; }

        //public int? MaDanhMuc { get; set; }
        public decimal? DonGia { get; set; }
        public int? SoLuongCon { get; set; }
        public int? TinhTrangKM { get; set; }
        public string Anh { get; set; }
        public string Mota { get; set; }//class lay ra nhug cai mjh muon n hien thi uk

    }
}