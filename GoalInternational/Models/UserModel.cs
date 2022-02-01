using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class UserModel
    {



        public int ID { get; set; }
        public int UserIDRequestedBy { get; set; }
        public int UserID { get; set; }
        public string UserType { get; set; }
        public int UserTypeID { get; set; }
        public string Language { get; set; }
        public string SpecifiedCulture { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string Username { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }



        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string ChequePayableTo { get; set; }

        
        public string EmailAddress { get; set; }
        public string ClinicName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string Address { get; set; }
        public string Address2 { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string City { get; set; }
       
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string Fax { get; set; }
        public string Specialty { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public int CountryID { get; set; }

        public string Country { get; set; }
        public bool? Activated { get; set; }
        public bool? Deleted { get; set; }
        public int Status { get; set; }
        public string StatusString { get; set; }
        public bool ConsentCHRCShareName { get; set; }

        public int MaxPatientsAllowed { get; set; }

        public bool IsReportAdmin { get; set; }
        public bool IsCountryLeader { get; set; }
        public bool ShowCustomLogo { get; set; }
        public int? ParentUserID { get; set; } 
    }
}