using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV1P1VisitDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GIntV1P1Model gModel = (GIntV1P1Model)validationContext.ObjectInstance;

            if(string.IsNullOrEmpty(gModel.VisitDate) || string.IsNullOrEmpty(gModel.PatientConsentDate))
            {
                return null;
            }

            DateTime consentDate;
            DateTime visitDate;
            DateTime notBeforeVisitDate;
            try
            {
                consentDate = DateTime.ParseExact(gModel.PatientConsentDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                visitDate = DateTime.ParseExact(gModel.VisitDate, "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
                notBeforeVisitDate = DateTime.ParseExact(ConfigurationManager.AppSettings["DateNotBefore"], "yyyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (FormatException e)
            {
                return null;
            }

            
            if (DateTime.Compare(visitDate.Date, notBeforeVisitDate.Date) < 0)
            {
                return new ValidationResult(GoalInternational.Languages.Common.DateNotBefore);
            }

            if (DateTime.Compare(visitDate.Date, consentDate.Date) < 0)
            {
                return new ValidationResult(GoalInternational.Languages.Common.VisitDateNotBeforeConsentDate);
            }

            return ValidationResult.Success;
        }
    }
}