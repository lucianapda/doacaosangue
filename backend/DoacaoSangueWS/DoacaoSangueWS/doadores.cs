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
    
    public partial class doadores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public doadores()
        {
            this.doacoes = new HashSet<doacoes>();
            this.hemocentros_doadores = new HashSet<hemocentros_doadores>();
        }
    
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public System.DateTime data_nascimento { get; set; }
        public string tipo_sanguineo { get; set; }
        public Nullable<double> peso { get; set; }
        public Nullable<double> altura { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<doacoes> doacoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<hemocentros_doadores> hemocentros_doadores { get; set; }
    }
}
