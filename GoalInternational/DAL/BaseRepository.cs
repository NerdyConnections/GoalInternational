using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoalInternational.Data;
namespace GoalInternational.DAL
{
    public class BaseRepository
    {

        private GoalInternationalEntities ent = null;

        public GoalInternationalEntities Entities
        {
            get
            {
                if (ent == null)
                {
                    ent = new GoalInternationalEntities();
                }
                return ent;

            }
        }
    }
}