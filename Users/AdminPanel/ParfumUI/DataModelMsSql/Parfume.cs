//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParfumUI.DataModelMsSql
{
    using System;
    using System.Collections.Generic;
    
    public partial class Parfume
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Parfume()
        {
            this.CategoryToParfums = new HashSet<CategoryToParfum>();
            this.SalePrices = new HashSet<SalePrice>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BrendId { get; set; }
        public int GenderId { get; set; }
        public int DensityId { get; set; }
        public byte[] Image { get; set; }
    
        public virtual Brend Brend { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryToParfum> CategoryToParfums { get; set; }
        public virtual Density Density { get; set; }
        public virtual Gender Gender { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalePrice> SalePrices { get; set; }
    }
}
