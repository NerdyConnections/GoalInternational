using GoalInternational.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GoalInternational.Util
{
    public class ListHelper
    {

        public static IEnumerable<SelectListItem> GetCountryList()
        {
            List<SelectListItem> CountryList = new List<SelectListItem>
            {

                    new SelectListItem {Text = "Brazil" , Value = "1"},
                    new SelectListItem {Text = "Canada" , Value = "2"},
                    new SelectListItem {Text = "Kuwait" , Value = "3"},
                    new SelectListItem {Text = "Mexico" , Value = "4"},
                      new SelectListItem {Text = "Saudi Arabia" , Value = "5"},
                        new SelectListItem {Text = "United Arab Emirates" , Value = "6"}


            };
            return CountryList;

        }

        public static IEnumerable<SelectListItem> GetEthnicityList()
        {
            List<SelectListItem> EthnicityList = new List<SelectListItem>
            {

                   // new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G9 , Value = "Aboriginal Canadian (First Nations / Métis / Inuit)"},
                    new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G10 , Value = "Arabic / North African"},
                    new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G11 , Value = "Black"},
                    new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G12 , Value = "Caucasian / White"},
                      new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G13 , Value = "Hispanic"},
                        new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G14, Value = "East / South-East Asian"},
                         new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G15 , Value = "Multi-Racial"},
                        new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G16 , Value = "South Asian"},
                         new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G17, Value = "Other"}




            };
            return EthnicityList;

        }

        public static IEnumerable<SelectListItem> GetSmokingHistoryList()
        {
            List<SelectListItem> SmokingHistoryList = new List<SelectListItem>
            {

                    new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G18 , Value = "Never Smoked"},
                    new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G19 , Value = "Past Smoker"},
                    new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G20 , Value = "Current Smoker"}
                   




            };
            return SmokingHistoryList;

        }

        public static IEnumerable<SelectListItem> GetMedicationInsuranceCoverageList()
        {
            List<SelectListItem> MedicationInsuranceCoverageList = new List<SelectListItem>
            {

                    new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G21 , Value = "Private"},
                    new SelectListItem {Text = @GoalInternational.Languages.GIntV1P2.G22 , Value = "Public"}
      
            };
            return MedicationInsuranceCoverageList;

        }
        public static IEnumerable<SelectListItem> GetT2DMDurations()
        {
            List<SelectListItem> T2DMDurations = new List<SelectListItem>
            {

                    new SelectListItem {Text = @GoalInternational.Languages.Common.year_less_1 , Value = @GoalInternational.Languages.Common.year_less_1},
 new SelectListItem {Text = @GoalInternational.Languages.Common.year_1 , Value = @GoalInternational.Languages.Common.year_1},
                    new SelectListItem {Text = @GoalInternational.Languages.Common.years_2 , Value = @GoalInternational.Languages.Common.years_2},
 new SelectListItem {Text = @GoalInternational.Languages.Common.years_3 , Value = @GoalInternational.Languages.Common.years_3},
 new SelectListItem {Text = @GoalInternational.Languages.Common.years_4 , Value = @GoalInternational.Languages.Common.years_4},
                       new SelectListItem {Text = @GoalInternational.Languages.Common.years_5 , Value = @GoalInternational.Languages.Common.years_5},
                        new SelectListItem {Text = @GoalInternational.Languages.Common.years_6 , Value = @GoalInternational.Languages.Common.years_6},
                         new SelectListItem {Text = @GoalInternational.Languages.Common.years_7 , Value = @GoalInternational.Languages.Common.years_7},
                          new SelectListItem {Text = @GoalInternational.Languages.Common.years_8 , Value = @GoalInternational.Languages.Common.years_8},
                           new SelectListItem {Text = @GoalInternational.Languages.Common.years_9 , Value = @GoalInternational.Languages.Common.years_9},
                            new SelectListItem {Text = @GoalInternational.Languages.Common.years_10 , Value = @GoalInternational.Languages.Common.years_10},
                             new SelectListItem {Text = @GoalInternational.Languages.Common.years_11 , Value = @GoalInternational.Languages.Common.years_11},
                              new SelectListItem {Text = @GoalInternational.Languages.Common.years_12 , Value = @GoalInternational.Languages.Common.years_12},
                               new SelectListItem {Text = @GoalInternational.Languages.Common.years_13 , Value = @GoalInternational.Languages.Common.years_13},
                                new SelectListItem {Text = @GoalInternational.Languages.Common.years_14 , Value = @GoalInternational.Languages.Common.years_14},
                                 new SelectListItem {Text = @GoalInternational.Languages.Common.years_15 , Value = @GoalInternational.Languages.Common.years_15},
                                  new SelectListItem {Text = @GoalInternational.Languages.Common.years_16 , Value = @GoalInternational.Languages.Common.years_16},
                                   new SelectListItem {Text = @GoalInternational.Languages.Common.years_17 , Value = @GoalInternational.Languages.Common.years_17},
                                    new SelectListItem {Text = @GoalInternational.Languages.Common.years_18 , Value = @GoalInternational.Languages.Common.years_18},
                                     new SelectListItem {Text = @GoalInternational.Languages.Common.years_19 , Value = @GoalInternational.Languages.Common.years_19},
                                      new SelectListItem {Text = @GoalInternational.Languages.Common.years_20 , Value = @GoalInternational.Languages.Common.years_20},
                                       new SelectListItem {Text = @GoalInternational.Languages.Common.years_21 , Value = @GoalInternational.Languages.Common.years_21},
                                        new SelectListItem {Text = @GoalInternational.Languages.Common.years_22 , Value = @GoalInternational.Languages.Common.years_22},
                                         new SelectListItem {Text = @GoalInternational.Languages.Common.years_23 , Value = @GoalInternational.Languages.Common.years_23},
                                          new SelectListItem {Text = @GoalInternational.Languages.Common.years_24 , Value = @GoalInternational.Languages.Common.years_24},
                                           new SelectListItem {Text = @GoalInternational.Languages.Common.years_25 , Value = @GoalInternational.Languages.Common.years_25},
                                            new SelectListItem {Text = @GoalInternational.Languages.Common.years_greater_25 , Value = @GoalInternational.Languages.Common.years_greater_25},



            };
            return T2DMDurations;

        }

        public static IEnumerable<SelectListItem> GetStatinTherapyList()
        {
            List<SelectListItem> StatinTherapyList = new List<SelectListItem>
            {

                    new SelectListItem {Text = @GoalInternational.Languages.Common.Atorvastatin , Value = "Atorvastatin"},
                    new SelectListItem {Text = @GoalInternational.Languages.Common.Fluvastatin , Value = "Fluvastatin"},
                    new SelectListItem {Text = @GoalInternational.Languages.Common.Lovastatin , Value = "Lovastatin"},
                    new SelectListItem {Text = @GoalInternational.Languages.Common.Pravastatin , Value = "Pravastatin"},
                      new SelectListItem {Text = @GoalInternational.Languages.Common.Pitavastatin , Value = "Pitavastatin"},
                    new SelectListItem {Text = @GoalInternational.Languages.Common.Rosuvastatin , Value = "Rosuvastatin"},
                    new SelectListItem {Text = @GoalInternational.Languages.Common.Simvastatin , Value = "Simvastatin"},
                    new SelectListItem {Text = @GoalInternational.Languages.Common.NoStatin , Value = "No Statin"}
                    


            };
            return StatinTherapyList;

        }
        public static IEnumerable<SelectListItem> GetLFUReasons()
        {
            List<SelectListItem> LFUReasons = new List<SelectListItem>
            {

                    new SelectListItem {Text = GoalInternational.Languages.GIntV2P1.G12 , Value = "Patient Died"},
                    new SelectListItem {Text = GoalInternational.Languages.GIntV2P1.G13 , Value = "Patient is Lost to follow-up (moved, no longer being followed-up by physician)"},
                    new SelectListItem {Text = GoalInternational.Languages.GIntV2P1.G14 , Value = "Patient withdrew consent"},
                    new SelectListItem {Text = GoalInternational.Languages.GIntV2P1.G15 , Value = "Physician took patient out"}
                      // new SelectListItem {Text = GoalInternational.Languages.GIntV2P1.G16 , Value = "No show for this visit"}




            };
            return LFUReasons;

        }

    }
}