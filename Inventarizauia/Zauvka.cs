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
    
    public partial class Zauvka
    {
        public int zauvkaId { get; set; }
        public Nullable<int> sotrudnikiId { get; set; }
        public Nullable<int> oborudovanieId { get; set; }
        public Nullable<int> mestoYstanovkiId { get; set; }
        public Nullable<System.DateTime> data { get; set; }
        public Nullable<int> statusZauvkaId { get; set; }
    
        public virtual MestoYstanovki MestoYstanovki { get; set; }
        public virtual Oborudovanie Oborudovanie { get; set; }
        public virtual Sotrudniki Sotrudniki { get; set; }
        public virtual StatusZauvka StatusZauvka { get; set; }
    }
}
