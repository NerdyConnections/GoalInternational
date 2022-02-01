using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class PayeeModel
    {



        public int UserID { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string PaymentMethod { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string ChequePayableTo { get; set; } 
        public string InternalRefNum { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string MailingAddr1 { get; set; }
        public string MailingAddr2 { get; set; }
        public string AttentionTo { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string City { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string Province { get; set; }
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        public string PostalCode { get; set; }
        public string TaxNumber { get; set; }
        public string AdditionalInformation { get; set; }
    }
}