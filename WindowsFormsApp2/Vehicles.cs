//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vehicles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicles()
        {
            this.Rentals = new HashSet<Rentals>();
        }
    
        public int vehicle_code { get; set; }
        public int brand_code { get; set; }
        public string reg_number { get; set; }
        public string body_num { get; set; }
        public string engine_num { get; set; }
        public string year { get; set; }
        public byte[] image { get; set; }
        public Nullable<double> vehicle_price { get; set; }
        public double rent_price { get; set; }
        public System.DateTime last_inspection { get; set; }
        public Nullable<int> employee_code { get; set; }
        public bool returned { get; set; }
    
        public virtual Brands Brands { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rentals> Rentals { get; set; }
    }
}
