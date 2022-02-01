using GoalInternational.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Resources;
using System.Web;

namespace GoalInternational.Models
{
    public class GIntV2SubmitModel
    {



        public int UserID { get; set; }
        public int PatientID { get; set; }
        public bool CanUserSubmit { get; set; }




        public bool? SignOff  { get; set; }
    }
}