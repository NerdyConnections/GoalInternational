using GoalInternational.Models;
using System;
using System.Linq;
using GoalInternational.Data;
using GoalInternational.Util;
using System.Collections.Generic;
using System.Configuration;


namespace GoalInternational.DAL
{
    public class SupportRepository: BaseRepository
    {
        public InviteesMaster AddInvited(SupportModel model, string userType, UserModel currentUser)
        {
            if (!CheckInvitedExists(model.Email))
            {
                try
                {
                    //UserRepository userRepo = new UserRepository();
                    //Models.UserModel currentUser = userRepo.GetUserDetails(System.Web.HttpContext.Current.User.Identity.Name);
                    //Models.UserModel currentUser = UserHelper.GetLoggedInUser();

                    InviteesMaster invited = new InviteesMaster();
                    invited.UserType = userType;
                    invited.Email = model.Email;
                    invited.FirstName = model.FirstName;
                    invited.LastName = model.LastName;

                    invited.ParentUserID = currentUser.UserID;
                    invited.Institution = currentUser.ClinicName;
                    invited.Address = currentUser.Address;
                    invited.Suite = currentUser.Address2;
                    invited.City = currentUser.City;
                    invited.Province = currentUser.Province;

                    invited.LastUpdated = DateTime.Now;
                    invited.AssignedPassword = ConfigurationManager.AppSettings["password"];

                    Entities.InviteesMasters.Add(invited);
                    Entities.SaveChanges();

                    return invited;
                }catch(Exception e)
                {
                    return null;
                }
            }else
            {
                return null;
            }
        }


        public bool CheckInvitedExists(string email)
        {
            var val = Entities.InviteesMasters.Where(x => x.Email == email).FirstOrDefault();
            if (val != null)
            {
                return true;
            }
            else
            {          
                return false;
            }

        }

        public List<InviteesMaster> GetAllInvitees(string userType)
        {
            UserModel currentUser = UserHelper.GetLoggedInUser();
            if (currentUser != null)
            {
                return Entities.InviteesMasters.Where(x => x.ParentUserID == currentUser.UserID 
                && x.UserType == userType
                && x.ParentUserID != x.UserID).ToList();
            }else
            {
                return null;
            }
        }

        public bool DeleteInvitee(int id)
        {
            try
            {
                InviteesMaster invitee = Entities.InviteesMasters.Where(x => x.id == id).FirstOrDefault();
                if (invitee != null)
                {
                    Entities.InviteesMasters.Remove(invitee);
                    Entities.SaveChanges();
                    return true;
                }else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}