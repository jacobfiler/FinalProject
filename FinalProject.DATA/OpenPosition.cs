//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject.DATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class OpenPosition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpenPosition()
        {
            this.Applications = new HashSet<Application>();
        }
    
        public int OpenPositionID { get; set; }
        public int LocationID { get; set; }
        public int PositionID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Application> Applications { get; set; }
        public virtual Location Location { get; set; }
        public virtual Position Position { get; set; }
    }
}
