using ShopOnline.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Entities
{
    public class CartItem
    {
        public Sach Sachs { get; set; }
        public int Soluong { get; set; }
    }
}