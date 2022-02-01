using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GoalInternational.CustomValidation
{
    public class ValidateV2LDLCYesNo:ValidationAttribute
    {

       
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var gModel = (GIntV2P1Model) validationContext.ObjectInstance;
                if (gModel.IsPatientLFU == "No" )
                {
                //cannot use required attribute at model because if lost to follow up is true no need to enter ldlc
                    if ((gModel.LDLCYesNo == "No") || string.IsNullOrEmpty(gModel.LDLCYesNo))
                    {
                        return new ValidationResult(GoalInternational.Languages.Common.LDLCYesNoErrMsg);
                    }

                    else
                        return ValidationResult.Success;
                }else
                return ValidationResult.Success;
        }
        
    }
}