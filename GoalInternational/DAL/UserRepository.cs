using GoalInternational.Data;
using GoalInternational.Models;
using GoalInternational.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace GoalInternational.DAL
{
    public class UserRepository : BaseRepository
    {
        //if 0 return - new user update password and complete registration
        //if 1 user authenticated successfully
        //if 2 user unable to be authenticated (either firstuser or normal user) or user is no longer active (deleted) or isactive==false
        public int Authenticate(string userName, string password)
        {

            try
            {
                //is this the first time

                var user = Entities.Users.Where(x => x.Username == userName).SingleOrDefault();
                if (user != null)
                {

                    //repeat user

                    string encryptedPassword;
                    encryptedPassword = Util.Encryptor.Encrypt(password);
                    var IsValidUser = Entities.Users.Where(x => x.Username == userName && x.Password == encryptedPassword).SingleOrDefault();
                    if (IsValidUser != null)
                    {
                        if (user.IsActive)
                        {

                            return 1;//valid and active user
                        }
                        else
                        {
                            return 2; //valid but no longer active
                        }
                    }
                    else
                    {
                        return 2;//incorrect username/password combo

                    }

                }
                else
                {
                    //potential firstname user since it is not in the user table, check invitee table

                    var IsValidFirstUser = Entities.InviteesMasters.Where(x => x.Email == userName && x.AssignedPassword == password).SingleOrDefault();
                    //first time user : update password and complete registration
                    if (IsValidFirstUser != null)
                    {
                        return 0;  //valid firstuser
                    }
                    else
                    {
                        return 2;  //unable to validate the first user

                    }
                }

            }catch (Exception e)
            {

                Console.WriteLine("Error: " + e.Message);
                return 2;
            }

        }

        public string GetRoles(string userName)
        {
            string retStr = string.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (string str in Entities.Users.First(u => u.Username == userName).Roles.Select(r => r.Name).ToList())
            {
                sb.Append(str).Append("|");

            }

            if (sb.Length > 0)
                retStr = sb.ToString().TrimEnd("|".ToCharArray());

            return retStr;
        }
        public string[] GetRolesAsArray(string userName)
        {
            return Entities.Users.First(u => u.Username == userName).Roles.Select(r => r.Name).ToArray();

        }
        public UserModel GetUserDetails(string username)
        {
            var user = Entities.Users.Where(x => x.Username == username).SingleOrDefault();
            int userID = 0;
            bool showCustomLogo = false;
           
            string language = string.Empty;
            int defaultMaxPatientAllowed = int.Parse(ConfigurationManager.AppSettings["PhysicianMaxPatientsAllowed"]);
            int m_MaxPatientsAllowed = defaultMaxPatientAllowed;
            if (user != null)
            {
                //english=eng
                //spanish=spa
                //portuguese=por
                language = user.LanguagePref;
                userID = user.UserID;

                m_MaxPatientsAllowed = user.MaxPatientsAllowed ?? defaultMaxPatientAllowed;
            }
            GoalInternational.Data.InviteesMaster userInfo = new GoalInternational.Data.InviteesMaster();

            if (userID == 0)
            {//user who have not register with us (ie. confirm me page)
                userInfo = Entities.InviteesMasters.Where(x => x.Email == username).SingleOrDefault();
            }
            else
            {//register user logging in
                userInfo = Entities.InviteesMasters.Where(x => x.UserID == userID).SingleOrDefault();
            }

            bool isCountryLeader = false;
            bool isReportAdmin = false;
                       
            string[] countryLeaders = ConfigurationManager.AppSettings["CountryLeaders"].Split(',');
            string[] reportAdmins = ConfigurationManager.AppSettings["ReportAdmins"].Split(',');
            string[] customLogoUserIDs = ConfigurationManager.AppSettings["customLogoUserIDs"].Split(',');
            List<int> lstCustomLogoUserIDs = new List<int>(); ;//contains both PI and SubI and SC userid

            if (userID > 0)
            {//test if the user should show the customLogo

                foreach (var item in customLogoUserIDs)
                {
                    List<int> lstCustomLogoChidrenUserIDs = null;
                    int PIUserID = Int32.Parse(item);
                    lstCustomLogoUserIDs.Add(PIUserID);//first add the PI userid
                    lstCustomLogoChidrenUserIDs = GetAllChildrenUserIDs(PIUserID);//then add all children userid pertaining to the pi userid
                    lstCustomLogoUserIDs.AddRange(lstCustomLogoChidrenUserIDs);

                }
                if (lstCustomLogoUserIDs.Contains(userID))
                {

                    showCustomLogo = true;
                }
            }

            if (Array.IndexOf(countryLeaders, username) >= 0)
            {
                isCountryLeader = true;
            }
            if (Array.IndexOf(reportAdmins, username) >= 0)
            {
                isReportAdmin = true;
            }

            if (userInfo != null)
            {
                //need to comment out below because  SI doesn't have countryID and will cause exception
               // string countryName = Entities.Countries.Where(x => x.CountryID == userInfo.CountryID).SingleOrDefault().CountryName;
                return new UserModel()
                {
                    UserID = userInfo.UserID ?? 0,
                    // TerritoryID = userInfo.TerritoryID,
                    Username = username,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    Language = language,
                    EmailAddress = userInfo.Email,
                    ClinicName = userInfo.Institution,
                    Address = userInfo.Address,
                    // Password= Util.Encryptor.Decrypt(user.Password),
                    City = userInfo.City,
                    // Province = userInfo.Province,
                    PostalCode = userInfo.PostalCode,
                    Phone = userInfo.PhoneNumber,
                    Fax = userInfo.FaxNumber,
                    Specialty = userInfo.Specialty,
                    UserType = userInfo.UserType,
                    CountryID = userInfo.CountryID??0,
                    Country=(userInfo.CountryID == null)? "" : userInfo.Country.CountryName,
                    MaxPatientsAllowed = m_MaxPatientsAllowed,

                    IsCountryLeader = isCountryLeader,
                    IsReportAdmin = isReportAdmin,
                    ShowCustomLogo = showCustomLogo,
                    ParentUserID = userInfo.ParentUserID

                };//return usermodel object
            }
            else
                return null;
        }

        public string GetUserPassword(string username)
        {
            var user = Entities.Users.Where(x => x.Username == username).SingleOrDefault();
            string password=string.Empty;
           
            if (user != null)
            {
                password = Util.Encryptor.Decrypt(user.Password);
            }
            return password;
        }

        public string GetPIFullName(UserModel user)
        {
            if(!user.UserType.Equals("PI") && user.ParentUserID.HasValue)
            {
                var pi = Entities.InviteesMasters.Where(x => x.UserID == user.ParentUserID.Value).SingleOrDefault();
                if (pi != null)
                {
                    return pi.FirstName + " " + pi.LastName;
                }
               
            }
            return user.FirstName + " " + user.LastName;
        }
        public string GetPICountry(UserModel user)
        {
            if (user.UserType != "PI" && user.ParentUserID.HasValue)
            {
                var pi = Entities.InviteesMasters.Where(x => x.UserID == user.ParentUserID.Value).SingleOrDefault();
                if (pi != null)
                {
                    return pi.Country.CountryName;
                }

            }
            return user.Country;
        }

        public InviteesMaster GetParentPI(UserModel user)
        {
            if (!user.UserType.Equals("PI") && user.ParentUserID.HasValue)
            {
                return Entities.InviteesMasters.Where(x => x.UserID == user.ParentUserID.Value).SingleOrDefault();               
            }
            return null;
        }
        public  List<int> GetAllChildrenUserIDs(int ParentUserID)
        {
            List<int> ChildrenUserIDs = null;

            ChildrenUserIDs = (from im in Entities.InviteesMasters
                               where im.ParentUserID == ParentUserID && im.UserID.HasValue
                               // display all requests for now where pr.RequestStatus == 2 || pr.RequestStatus == 3  //show only submitted requests (either approved 3 or waiting to be approved  2 in the admin tool)

                               select im.UserID??0).ToList();

            return ChildrenUserIDs;
        }
    }


}
