//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoalInternational.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PatientVisit
    {
        public int id { get; set; }
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> GIntV1Status { get; set; }
        public Nullable<System.DateTime> GIntV1CompletionDate { get; set; }
        public Nullable<int> GIntV1SavedPage { get; set; }
        public Nullable<int> GIntV2Status { get; set; }
        public Nullable<System.DateTime> GIntV2CompletionDate { get; set; }
        public Nullable<int> GIntV2SavedPage { get; set; }
        public string GIntV2LFUReason { get; set; }
        public Nullable<int> GIntV3Status { get; set; }
        public Nullable<System.DateTime> GIntV3CompletionDate { get; set; }
        public Nullable<int> GIntV3SavedPage { get; set; }
        public string GIntV3LFUReason { get; set; }
        public Nullable<int> GIntV4Status { get; set; }
        public Nullable<System.DateTime> GIntV4CompletionDate { get; set; }
        public Nullable<int> GIntV4SavedPage { get; set; }
        public string GIntV4LFUReason { get; set; }
        public Nullable<int> GIntVisit2ReminderSent { get; set; }
        public Nullable<int> GIntVisit3ReminderSent { get; set; }
        public Nullable<int> GIntVisit4ReminderSent { get; set; }
        public System.DateTime LastUpdated { get; set; }
    }
}