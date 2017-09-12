namespace ShopOnline.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTietHoaĐon = new HashSet<ChiTietHoaĐon>();
        }

        [Key]
        public int MaHoaDon { get; set; }

        public DateTime? ThoiGian { get; set; }

        public int? MaKH { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public int? MaNhanVien { get; set; }

        public bool? isdelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaĐon> ChiTietHoaĐon { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
