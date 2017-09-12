namespace ShopOnline.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SangTac")]
    public partial class SangTac
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaTG { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSach { get; set; }

        [StringLength(10)]
        public string TGSangTac { get; set; }

        public bool? isdelete { get; set; }

        public virtual Sach Sach { get; set; }

        public virtual TacGia TacGia { get; set; }
    }
}
