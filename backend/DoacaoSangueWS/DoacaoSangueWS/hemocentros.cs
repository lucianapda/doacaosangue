//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoacaoSangueWS
{
    using System;
    using System.Collections.Generic;
    
    public partial class hemocentros
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hemocentros()
        {
            this.doadores = new HashSet<doadores>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string estado { get; set; }
        public string cidade { get; set; }
        public Nullable<int> numero { get; set; }
        public string cep { get; set; }
        public string complemento { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<doadores> doadores { get; set; }
    }
}
