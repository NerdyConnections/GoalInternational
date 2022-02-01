using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV2Height : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var GModel = (GIntV2P2Model)validationContext.ObjectInstance;

            //didn't choose a membership type = 0 or choose pay as you go
            double n;
            if (GModel.Height != null)
            {
                bool isNumeric = double.TryParse(GModel.Height.ToString(), out n);
                if (isNumeric)
                {
                    if (GModel.HeightUnit == "cm")
                    {
                        if (Convert.ToDecimal(GModel.Height) >= 100M && (Convert.ToDecimal(GModel.Height) <= 230M))
                        {

                            return ValidationResult.Success;
                        }
                        else
                            return new ValidationResult(GoalInternational.Languages.Common.ValidHeightRange);
                    }
                    else//user enter height in inches
                    {
                        if (Convert.ToDecimal(GModel.Height) >= 39M && (Convert.ToDecimal(GModel.Height) <= 90M))
                        {

                            return ValidationResult.Success;
                        }
                        else
                            return new ValidationResult(GoalInternational.Languages.Common.ValidHeightRange);

                    }


                }
                else
                {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidHeightRange);
                }
            }
            else
            {
                //covid19
                return ValidationResult.Success;
               // return new ValidationResult(GoalInternational.Languages.Common.ValidHeightRange);
            }

        }
    }
}