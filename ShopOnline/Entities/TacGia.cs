namespace ShopOnline.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TacGia")]
    public partial class TacGia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TacGia()
        {
            SangTacs = new HashSet<SangTac>();
        }

        [Key]
        public int MaTG { get; set; }

        [StringLength(50)]
        public string TenTG { get; set; }

        [StringLength(1000)]
        public string ThongTinTG { get; set; }

        public bool? isdelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SangTac> SangTacs { get; set; }
    }
}
