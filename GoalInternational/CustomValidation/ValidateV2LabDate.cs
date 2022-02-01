using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV2LabDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV2P3Model gModel = (GIntV2P3Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.LabDate))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }
            else
            {



                DateTime Vist2Date;
                DateTime LabDate;
                try
                {
                    Vist2Date = DateTime.ParseExact(gModel.VisitDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    LabDate = DateTime.ParseExact(gModel.LabDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    if (Vist2Date.Date < LabDate.Date)
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.LabDateErrMsg);
                    }
                }
                catch (FormatException e)
                {
                    return new ValidationResult(GoalInternational.Languages.Common.LabDateErrMsg);
                }
            }

           

            return ValidationResult.Success;
        }  
    }
}