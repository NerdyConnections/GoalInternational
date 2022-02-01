using GoalInternational.Models;
using System.ComponentModel.DataAnnotations;

namespace GoalInternational.CustomValidation
{
    public class ValidateCoMorbidities : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gModel = (GIntV1P2Model)validationContext.ObjectInstance;

            if (gModel.CoMorbidities1 == "No" && gModel.CoMorbidities2 == "No"  && gModel.CoMorbidities3 == "No"  && gModel.CoMorbidities4 == "No" && gModel.CoMorbidities9 == "No")
            {
                return new ValidationResult(GoalInternational.Languages.Common.AtLeastOneCVD);
            }

            else
                return ValidationResult.Success;
        }
    }
}