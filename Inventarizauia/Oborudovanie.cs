//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inventarizauia
{
    using System;
    using System.Collections.Generic;
    
    public partial class Oborudovanie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Oborudovanie()
        {
            this.Zauvka = new HashSet<Zauvka>();
        }
    
        public int oborudovanieId { get; set; }
        public string title { get; set; }
        public Nullable<System.DateTime> dataPokypki { get; set; }
        public Nullable<int> sotrudnikiId { get; set; }
        public Nullable<int> proizvoditelId { get; set; }
        public Nullable<int> tipEquipmentId { get; set; }
        public Nullable<int> mestoYstanovkiId { get; set; }
        public Nullable<int> statusOborudovaniaId { get; set; }
    
        public virtual MestoYstanovki MestoYstanovki { get; set; }
        public virtual Proizvoditel Proizvoditel { get; set; }
        public virtual Sotrudniki Sotrudniki { get; set; }
        public virtual StatusOborudovania StatusOborudovania { get; set; }
        public virtual TipEquipment TipEquipment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zauvka> Zauvka { get; set; }
    }
}
