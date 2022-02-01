using System.ComponentModel.DataAnnotations;

namespace GoalInternational.Models
{
    public class NotMeModel
    {
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Clinic Name")]
        public string ClinicName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Province")]
        public string Province { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "EMAIL_INVALID")]
        public string Email { get; set; }
    }
}