using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateWaist : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var GModel = (GIntV1P3Model)validationContext.ObjectInstance;
            
            //didn't choose a membership type = 0 or choose pay as you go
            double n;

            if (GModel.WaistCircumferenceUnit != "N/A")
            {
                
                if (GModel.WaistCircumference != null)
                {
                    bool isNumeric = double.TryParse(GModel.WaistCircumference.ToString(), out n);
                    if (isNumeric)
                    {
                        if (GModel.WaistCircumferenceUnit == "cm")
                        {
                            if (Convert.ToDecimal(GModel.WaistCircumference) >= Convert.ToDecimal(45) && (Convert.ToDecimal(GModel.WaistCircumference) <= Convert.ToDecimal(200)))
                            {

                                return ValidationResult.Success;
                            }
                            else
                                return new ValidationResult(GoalInternational.Languages.Common.ValidWaistRange);
                        }
                        else//user enter weight in inches
                        {
                            if (Convert.ToDecimal(GModel.WaistCircumference) >= Convert.ToDecimal(18) && (Convert.ToDecimal(GModel.WaistCircumference) <= Convert.ToDecimal(80)))
                            {

                                return ValidationResult.Success;
                            }
                            else
                                return new ValidationResult(GoalInternational.Languages.Common.ValidWaistRange);

                        }


                    }
                    else//if waist value is not a valid number
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.ValidWaistRange);
                    }
                }
                else//if waist value is blank
                {
                    return new ValidationResult(GoalInternational.Languages.Common.ValidWaistRange);
                }
            }
            else{//if user select N/A

                return ValidationResult.Success;
            }

        }
    }
}