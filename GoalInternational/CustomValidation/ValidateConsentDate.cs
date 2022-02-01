using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateConsentDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV1P1Model gModel = (GIntV1P1Model)validationContext.ObjectInstance;

            if (string.IsNullOrEmpty(gModel.PatientConsentDate))
            {
                return null;
            }

            DateTime consentDate;         
            DateTime notBefore;
            try
            {
                consentDate = DateTime.ParseExact(gModel.PatientConsentDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                notBefore = DateTime.ParseExact(ConfigurationManager.AppSettings["DateNotBefore"], "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                return null;
            }


            if (DateTime.Compare(consentDate.Date, notBefore.Date) < 0)
            {
                return new ValidationResult(GoalInternational.Languages.Common.DateNotBefore);
            }

            return ValidationResult.Success;
        }  
    }
}