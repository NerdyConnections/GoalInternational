using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV4LabDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV4P3Model gModel = (GIntV4P3Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.LabDate))
            {
                return new ValidationResult(GoalInternational.Languages.Common.RequiredFieldWithAsterisk);
            }
            else
            {



                DateTime Vist4Date;
                DateTime LabDate;
                try
                {
                    Vist4Date = DateTime.ParseExact(gModel.VisitDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    LabDate = DateTime.ParseExact(gModel.LabDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                    if (Vist4Date.Date < LabDate.Date)
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