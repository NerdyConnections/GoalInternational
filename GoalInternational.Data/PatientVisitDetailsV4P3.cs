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
    
    public partial class PatientVisitDetailsV4P3
    {
        public int id { get; set; }
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> LabDate { get; set; }
        public Nullable<decimal> TotalCholesterol { get; set; }
        public Nullable<decimal> LDLC { get; set; }
        public Nullable<decimal> HDLC { get; set; }
        public Nullable<decimal> NHDLC { get; set; }
        public Nullable<decimal> Triglycerides { get; set; }
        public Nullable<decimal> ApoB { get; set; }
        public string ApoBUnit { get; set; }
        public string StatinTherapy { get; set; }
        public string StatinDose { get; set; }
        public string LipidTherapy1 { get; set; }
        public string LipidTherapy2 { get; set; }
        public string LipidTherapy3 { get; set; }
        public string LipidTherapy4 { get; set; }
        public string LipidTherapy5 { get; set; }
        public string LipidTherapy6 { get; set; }
        public string ReasonNotEzetimibe { get; set; }
        public string ReasonNotPCSK9i { get; set; }
        public Nullable<System.DateTime> LastUpdated { get; set; }
        public string TotalCholesterolUnit { get; set; }
        public string LDLCUnit { get; set; }
        public string HDLCUnit { get; set; }
        public string NHDLCUnit { get; set; }
        public string TriglyceridesUnit { get; set; }
    }
}
