//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TICRM.MQTT.Listner
{
    using System;
    using System.Collections.Generic;
    
    public partial class Device
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Device()
        {
            this.DeciveConfigurations = new HashSet<DeciveConfiguration>();
            this.DeviceSensors = new HashSet<DeviceSensor>();
        }
    
        public System.Guid DeviceId { get; set; }
        public string Name { get; set; }
        public string Mac { get; set; }
        public string EMEINumber { get; set; }
        public Nullable<System.DateTime> RegistrationDate { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public bool ServiceDateFlag { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<System.Guid> AccountId { get; set; }
        public Nullable<System.Guid> CustomerAssetId { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.Guid> StatusId { get; set; }
        public string CloudServices { get; set; }
        public string CloudData { get; set; }
        public string Maintenance { get; set; }
        public Nullable<System.Guid> AssignedUser { get; set; }
        public Nullable<System.Guid> AssignedTeam { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeciveConfiguration> DeciveConfigurations { get; set; }
        public virtual Status Status { get; set; }
        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceSensor> DeviceSensors { get; set; }
    }
}
