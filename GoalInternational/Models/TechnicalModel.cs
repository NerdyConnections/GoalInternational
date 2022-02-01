using System.ComponentModel.DataAnnotations;


namespace GoalInternational.Models
{
    public class TechnicalModel
    {
        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "EMAIL_INVALID")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Languages.Common), ErrorMessageResourceName = "RequiredFieldWithAsterisk")]
        [Display(Name = "Issue")]
        public string Issue { get; set; }
    }
}