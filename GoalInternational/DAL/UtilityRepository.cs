using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoalInternational.DAL
{
    public class UtilityRepository : BaseRepository
    {
        public List<string> GetStatinDosages(string StatinTherapy, string SpecifiedCulture)
        {
            List<string> StatinDosages = null;

            //head office and sale director see every sessions for a program for a sponsor no need to query for territoryID
            try
            {

                StatinDosages = (from stdl in Entities.StatinTherapyDosageLookups
                                 where stdl.StatinTherapy == StatinTherapy
                                 // display all requests for now where pr.RequestStatus == 2 || pr.RequestStatus == 3  //show only submitted requests (either approved 3 or waiting to be approved  2 in the admin tool)
                                
                                 select stdl.Dosage).ToList();


                return StatinDosages;
            }
            catch (Exception e)
            {
                return new List<string>();
            }
        }
    }
}