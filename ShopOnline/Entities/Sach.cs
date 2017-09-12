namespace ShopOnline.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sach")]
    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            ChiTietHoaĐon = new HashSet<ChiTietHoaĐon>();
            SangTacs = new HashSet<SangTac>();
        }

        [Key]
        public int MaSach { get; set; }

        [StringLength(50)]
        public string TenSach { get; set; }

        public int? MaTheLoai { get; set; }

        public int? MaNXB { get; set; }

        public int? MaDanhMuc { get; set; }

        [Column(TypeName = "money")]
        public decimal? DonGia { get; set; }

        public int? SoLuongCon { get; set; }

        public int? TinhTrangKM { get; set; }

        public string Anh { get; set; }

        public string Mota { get; set; }

        public bool? isdelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaĐon> ChiTietHoaĐon { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }

        public virtual NXB NXB { get; set; }

        public virtual TheLoaiSach TheLoaiSach { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SangTac> SangTacs { get; set; }
    }
}
