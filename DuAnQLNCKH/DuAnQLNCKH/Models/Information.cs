//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DuAnQLNCKH.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Information
    {
        public string IdLe { get; set; }
        public string NameLe { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Address { get; set; }
        public string CMND { get; set; }
        public string IdKhoa { get; set; }
        public Nullable<int> IdPo { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Position Position { get; set; }
    }
}
