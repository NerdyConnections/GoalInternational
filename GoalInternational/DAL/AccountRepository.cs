using GoalInternational.Models;
using GoalInternational.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoalInternational.Data;
using System.Configuration;

namespace GoalInternational.DAL
{
    public class AccountRepository :BaseRepository
    {
        public void RegisterUser(UserModel um)
        {

            User m_user = new User();


            int userID;
            m_user.Username = um.Username;
            m_user.Password = Encryptor.Encrypt(um.Password);
            m_user.Email = um.Username;
            m_user.FirstLogin = true;
            m_user.IsActive = true;
            m_user.LanguagePref = um.Language;
            m_user.ActivationDate = DateTime.Now;
            
            m_user.MaxPatientsAllowed= int.Parse(ConfigurationManager.AppSettings["PhysicianMaxPatientsAllowed"]);
            Entities.Users.Add(m_user);
            Entities.SaveChanges();
            userID = m_user.UserID;

            var userInfo = Entities.InviteesMasters.Where(x => x.Email == um.Username).SingleOrDefault();
            if (userInfo != null)
            {
                //  InviteesMaster userInfo = new InviteesMaster();
                userInfo.UserID = userID;
               // userInfo.ParentUserID = userID;
                //userInfo.UserType = "PI"; //physician
                userInfo.Email = um.Username;
                userInfo.FirstName = um.FirstName;
                userInfo.LastName = um.LastName;
                userInfo.PayableTo = um.ChequePayableTo;
                userInfo.Institution = um.ClinicName;
                userInfo.Address = um.Address;
                userInfo.Suite = um.Address2;
                userInfo.City = um.City;
                userInfo.Province = um.Province;
                userInfo.CountryID = um.CountryID;
                userInfo.PhoneNumber = um.Phone;
                userInfo.CellNumber = um.CellPhone;
                userInfo.FaxNumber = um.Fax;
                userInfo.Province = um.Province;
                userInfo.ConsentCHRCShareName = um.ConsentCHRCShareName;
                userInfo.RegistrationStatus = 2;//registered
                userInfo.LastUpdated = DateTime.Now;


                Entities.SaveChanges();
            }
            var payeeInfo = Entities.PayeeInfoes.Where(x => x.UserID == userID).SingleOrDefault();
            if (payeeInfo == null)
            {
                PayeeInfo pi = new PayeeInfo();
                pi.UserID = userID;
                pi.PaymentMethod = String.Empty; //by person or company, removed as Anatoly requested on March 05, 2019
                pi.ChequePayableTo = um.ChequePayableTo;
                // pi.InternalRefNum = pm.InternalRefNum;  removed as Anatoly requested on March 05, 2019
                pi.MailingAddr1 = um.Address;
               // pi.MailingAddr2 = pm.MailingAddr2;
               // pi.AttentionTo = pm.AttentionTo;
                pi.City = um.City;
                pi.Province = um.Province;
                pi.PostalCode = um.PostalCode;
               // pi.TaxNumber = um.TaxNumber;
               // pi.AdditionalInstructions = pm.AdditionalInformation;
                pi.LastUpdated = DateTime.Now;

                Entities.PayeeInfoes.Add(pi);
                Entities.SaveChanges();

            }
            um.UserID = userID;
            um.Country = Entities.Countries.Where(x => x.CountryID == um.CountryID).SingleOrDefault().CountryName;
        }
        public bool CheckUsernameExists(string emailAddress)
        {

            var val = Entities.Users.Where(x => x.Username == emailAddress).FirstOrDefault();
            if (val != null)
            {

                return true;
            }
            else
            {

                return false;
            }


        }
    }
}