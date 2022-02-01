using GoalInternational.Languages;
using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalInternational.CustomValidation
{

   

    public class ValidateWeight : ValidationAttribute, IClientValidatable
    {
       public  string WeightUnit { get; set; }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            //When the MVC framework finds a validation object with this interface present, it invokes GetClientValidationRules to retrieve a sequence of ModelClientValidationRule objects. These objects carry the metadata, or the rules, the framework sends to the client.

            //get error message from model (ie. Languages.Common.ValidWeightRange)
            string errorMessage = ErrorMessageString;
           
            // The value we set here are needed by the jQuery adapter
            ModelClientValidationRule ValidWeightRange = new ModelClientValidationRule();
            ValidWeightRange.ErrorMessage = errorMessage;
            //javascript code that do the validation
            ValidWeightRange.ValidationType = "checkvalidweight"; // This is the name the jQuery adapter will use
                                                                  //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
                                                                  // dateGreaterThanRule.ValidationParameters.Add("otherpropertyname", otherPropertyName);
            ValidWeightRange.ValidationParameters.Add("weightunit", WeightUnit);//Validation parameter names in unobtrusive client validation rules must start with a lowercase letter and consist of only lowercase letters or digits
            yield return ValidWeightRange;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var GModel = (GIntV1P3Model)validationContext.ObjectInstance;

            WeightUnit = GModel.WeightUnit;

            //test to see if it is a valid number
            double n;
            if (GModel.Weight != null)
            {
                bool isNumeric = double.TryParse(GModel.Weight.ToString(), out n);
               // if (isNumeric)
              //  {
                    if (GModel.WeightUnit == "lbs")
                    {
                        if (Convert.ToDecimal(GModel.Weight) >= Convert.ToDecimal(66) && (Convert.ToDecimal(GModel.Weight) <= Convert.ToDecimal(660))){

                            return ValidationResult.Success;
                        }
                        else
                            return new ValidationResult(GoalInternational.Languages.Common.ValidWeightRange);
                    }
                    else//user enter weight in kgs
                    {
                        if (Convert.ToDecimal(GModel.Weight) >= Convert.ToDecimal(30) && (Convert.ToDecimal(GModel.Weight) <= Convert.ToDecimal(300)))
                        {

                            return ValidationResult.Success;
                        }
                        else
                            return new ValidationResult(GoalInternational.Languages.Common.ValidWeightRange);

                    }
                  

               // }
               // else
               // {
               //     return new ValidationResult(GoalInternational.Languages.Common.ValidWeightRange);
               // }
            }
            else
            {
                return new ValidationResult(GoalInternational.Languages.Common.ValidWeightRange);
            }

        }
    }
}