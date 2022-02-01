using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoalInternational.Models
{
    public class UserReportModel
    {
        public int? ParentUserID { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; } //Principal Investigator or Sub Investigator
       public int? CountryID { get; set; }
        public int PatientID { get; set; }
        public string CountryName { get; set; }
           
            public int? Visit4Status { get; set; }
        
          public string AllVisitsComplete { get; set; }

            //about user
           
           
           
           
           
           
          
  
        }
}