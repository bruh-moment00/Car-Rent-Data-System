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
    
    public partial class Rentals
    {
        public int code { get; set; }
        public System.DateTime issue_date { get; set; }
        public System.DateTime return_date { get; set; }
        public int vehicle_code { get; set; }
        public int client_code { get; set; }
        public Nullable<int> option_code1 { get; set; }
        public Nullable<int> option_code2 { get; set; }
        public Nullable<int> option_code3 { get; set; }
        public Nullable<int> rental_period { get; set; }
        public double rental_cost { get; set; }
        public double payment_mark { get; set; }
        public int employee_code { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Employees Employees { get; set; }
        public virtual Options Options { get; set; }
        public virtual Options Options1 { get; set; }
        public virtual Options Options2 { get; set; }
        public virtual Vehicles Vehicles { get; set; }
    }
}
