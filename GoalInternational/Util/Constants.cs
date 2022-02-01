using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoalInternational.Util
{
    
        public static class Constants
        {
           

            public static readonly string USER = "USER";


        public static readonly string Admin = "Admin";

        public static readonly string NA = "N/A";
     
            public enum UserRole
            {
                Admin = 1,  //Administrator
                PI = 2,//Principal investigator
                SI = 3,//Sub Investigator
                SC = 4//Study Coordinator
                

            }
       

        }
    
}