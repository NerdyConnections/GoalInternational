using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using System.Globalization;
using GoalInternational.Models;
using GoalInternational.DAL;
using System;
using System.Configuration;
using System.Collections.Generic;
using GoalInternational.Data;

namespace GoalInternational.Util
{
    public static class UserHelper
    {



        #region Andrew'sCode
        private static void LoadDataIntoSession()
        {
            // PhysicianController usrControler = new PhysicianController();
            UserRepository UserRepo = new UserRepository();
            Models.UserModel user = UserRepo.GetUserDetails(System.Web.HttpContext.Current.User.Identity.Name);

            if (user != null)
            {
                if (user.Language == "spa")
                    user.SpecifiedCulture = "es-MX";
                else if (user.Language == "por")
                    user.SpecifiedCulture = "pt-BR";
                else
                    user.SpecifiedCulture = "en-US";

                HttpContext.Current.Session[Constants.USER] = user;
            }
        }

        public static UserModel GetLoggedInUser()
        {
            if (HttpContext.Current.Session[Constants.USER] == null)

                LoadDataIntoSession();

            return HttpContext.Current.Session[Constants.USER] as UserModel;

        }

        public static UserModel GetLoggedInUser(Controller controller)  //fix HttpContext.Current null when using multiple threads
        {
            UserRepository UserRepo = new UserRepository();

            Models.UserModel user = UserRepo.GetUserDetails(controller.HttpContext.User.Identity.Name);

            if (user != null)
            {
                if (user.Language == "spa")
                    user.SpecifiedCulture = "es-MX";
                else if (user.Language == "por")
                    user.SpecifiedCulture = "pt-BR";
                else
                    user.SpecifiedCulture = "en-US";
            }
            return user;
        }
    
       

        //pass in a comma delimited string of roles and determine if current user is in any one of them
        public static bool IsInRole(string roles)
        {

            String[] ArRoles = roles.Split(',');
            var user = HttpContext.Current.User;
            foreach (string role in ArRoles)
            {
                if (user.IsInRole(role))
                    return true;
            }

            return false;

        }
        public static void SetLoggedInUser(UserModel user, System.Web.SessionState.HttpSessionState session)
        {

            session[Constants.USER] = user;

        }

        public static bool SendEmailToAdmin(string emailTo, string emailBody, string emailSubject)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["emailGeneral"]);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(emailTo));
                //testing  mailMessage.To.Add(new System.Net.Mail.MailAddress("amanullaha@chrc.net"));
                mailMessage.Subject = emailSubject;

                mailMessage.IsBodyHtml = true;
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(GetRegistrationEmailBody(string.Empty, string.Empty, string.Empty, string.Empty), null, "text/html");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailBody, null, "text/html");


                //LinkedResource imagelink = new LinkedResource(Server.MapPath("~/images/regEmailImage.jpg"), "image/jpg");

                //imagelink.ContentId = "imageId";

                //imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;

                //htmlView.LinkedResources.Add(imagelink);

                mailMessage.AlternateViews.Add(htmlView);
                //  mailMessage.Attachments.Add(new Attachment(Server.MapPath("~/pdf/CHOLESTABETES Needs Assessment.pdf")));

                SendMail(mailMessage);
                return true;


            }

            catch (Exception e)
            {
                // Response.Write("fail in sendEmailNotification+++++" + e.Message.ToString());

                return false;
            }
        }
        public static HttpCookie GetAuthorizationCookie(string userName, string userData)
        {
            HttpCookie httpCookie = FormsAuthentication.GetAuthCookie(userName, true);
            FormsAuthenticationTicket currentTicket = FormsAuthentication.Decrypt(httpCookie.Value);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(currentTicket.Version, currentTicket.Name, currentTicket.IssueDate, currentTicket.Expiration, currentTicket.IsPersistent, userData);
            httpCookie.Value = FormsAuthentication.Encrypt(ticket);
            return httpCookie;
        }




        public static bool SendMail(MailMessage message)
        {
            SmtpClient client = new SmtpClient();
            try
            {

                client.Host = ConfigurationManager.AppSettings["smtpServer"];

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                // NetworkCred.UserName = "webmaster@questionaf.ca";
                //NetworkCred.Password = "xkc232v";
                NetworkCred.UserName = ConfigurationManager.AppSettings["smtpUser"];
                NetworkCred.Password = ConfigurationManager.AppSettings["smtpPassword"];
                client.UseDefaultCredentials = false;
                client.Credentials = NetworkCred;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                // client.Port = 25;
                client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);

                client.Timeout = 20000;
                client.EnableSsl = true;
                client.Send(message);

                return true;
            }
            catch (Exception e)
            {
                client = null;
                String Error = e.Message.ToString();
                //Utility.WriteToLog("SendMail Error: " + Error);

                return false;
            }

        }


        public static IEnumerable<SelectListItem> GetProvinces()
        {
            List<SelectListItem> provinces = new List<SelectListItem>
            {

                      new SelectListItem {Text = "AB", Value = "AB"},
                      new SelectListItem {Text = "BC", Value = "BC"},
                      new SelectListItem {Text = "MB", Value = "MB"},
                      new SelectListItem {Text = "NS", Value = "NS"},
                      new SelectListItem {Text = "NB", Value = "NB"},
                      new SelectListItem {Text = "NL", Value = "NL"},
                      new SelectListItem {Text = "ON", Value = "ON"},
                      new SelectListItem {Text = "PE", Value = "PE"},
                      new SelectListItem {Text = "QC", Value = "QC"},
                      new SelectListItem {Text = "SK", Value = "SK"},


            };
            return provinces;

        }
    
        private static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        #endregion

        #region Ali's Code =================================================================================================================================
        public static void SendEmailForgotPassword(string email, string password)
        {

            try
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                mailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["emailGeneral"]);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(email));
                mailMessage.Subject = Languages.Common.FORGOT_PWD_HEADER;
                mailMessage.IsBodyHtml = true;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(GetEmailBodyPassword(email, password), null, "text/html");
                mailMessage.AlternateViews.Add(htmlView);

                UserHelper.SendMail(mailMessage);
            }

            catch (Exception e)
            {

                string msg = e.Message;


            }


        }
        private static string GetEmailBodyPassword(string userName, string password)
        {
            string html = string.Empty;


            html = Languages.Common.FORGOTPASSWORD_TEXT;

            html = html.Replace("{userName}", userName);
            html = html.Replace("{password}", password);



            return html;

        }
      


        public static string GetFileNameForEmailAttachment(string path, int ProgramRequestID)
        {


            string retVal = "";




            if (!(string.IsNullOrEmpty(path)))
            {
                var FileName = Path.GetFileName(path);
                retVal = ProgramRequestID + "/Agenda/" + FileName;
            }
            return retVal;

        }
      
        public static string ConvertTimetoProperFormat(string date)
        {
            string retDate = "";

            if (!string.IsNullOrEmpty(date))
            {
                DateTime dt = DateTime.ParseExact(date, "yyyy/MM/dd", null);
                retDate = dt.ToLongDateString();
            }

            return retDate;
        }

        public static bool IsEmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static void SendActivationEmail(string FirstName, string email, string password)
        {

            try
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                mailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["emailGeneral"]);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(email));
                mailMessage.Subject = "Welcome to your personal CPD Portal: Your Account has been Successfully Activated";

                mailMessage.IsBodyHtml = true;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(GetActivationEmailBody(FirstName, email, password), null, "text/html");

                mailMessage.AlternateViews.Add(htmlView);
                mailMessage.Attachments.Add(new Attachment(HttpContext.Current.Server.MapPath("~/PDF/Cookies.pdf")));
                UserHelper.SendMail(mailMessage);
            }

            catch (Exception e)
            {

                string msg = e.Message;


            }


        }
        private static string GetActivationEmailBody(string FirstName, string userName, string password)
        {
            string html = string.Empty;

            html = Languages.Common.ActivationEmail;

            html = html.Replace("{FirstName}", FirstName);
            html = html.Replace("{userName}", userName);
            html = html.Replace("{password}", password);



            return html;

        }
    
        

        public static void AdminToSalesRep(string LocationName, UserModel um, string ProgramName, string ChosenDate)
        {
            try
            {


                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["AdminEmail"]);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(um.EmailAddress));
                mailMessage.Subject = "Changes Required" + ProgramName + " – Session Request:  " + ChosenDate;
                mailMessage.IsBodyHtml = true;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(AdminToSalesRepBody(LocationName, um, ProgramName, ChosenDate), null, "text/html");
                mailMessage.AlternateViews.Add(htmlView);

                UserHelper.SendMail(mailMessage);
            }

            catch (Exception e)
            {
                string msg = e.Message;
            }
        }
        private static string AdminToSalesRepBody(String LocationName, UserModel um, string ProgramName, string ChosenDate)
        {
            string html = string.Empty;

            html = @" <style>
					   
						.emailBodyWrapper
						{
							padding: 5px;
							font-family: Candara;
							 font-size:14px;
						}
					 li
							{
							  padding-top:28px;   
							}
					</style>
					<div class='emailBodyWrapper'>
						<p>
							Dear  {LastName},
						</p>
						<p>
							Please note that the selected venue, {VenueName}, is not available for the session date you had requested. 
						</p>
						
						<p>
						   Next Steps:
						   
						<p>
							1.	Please log in to your portal: https://amgen.ccpdhm.com  <br />
							2.	Visit your dashboard for the Program Name <br />
							3.	Click on the <strong>pencil</strong> icon under the <strong>Full Session Details</strong> column  <br />
							4.	Provide alternative venue details for this session <br /> 
						</p>
						
						<p>
							Please do not hesitate to contact us should you have any questions or require any assistance. <br />
							E: info@ccpdhm.com
						</p>
					   
					</div>";






            html = html.Replace("{ChosenDate}", ChosenDate);
            html = html.Replace("{LastName}", um.LastName);
            html = html.Replace("{VenueName}", LocationName);


            return html;

        }

        #endregion

        public static void SendEmailNotMe(NotMeModel model)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["emailGeneral"]);
                mailMessage.To.Add(new MailAddress(ConfigurationManager.AppSettings["CTUAdmin"]));
                mailMessage.Subject = "Not Me";

                mailMessage.Body = "First Name: " + model.FirstName + "\n"
                     + "Last Name: " + model.LastName + "\n"
                     + "Clinic Name: " + model.ClinicName + "\n"
                     + "Address: " + model.Address + "\n"
                     + "City: " + model.City + "\n"
                     + "Province: " + model.Province + "\n"
                     + "Postal Code: " + model.PostalCode + "\n"
                     + "Phone Number: " + model.PhoneNumber + "\n"
                     + "Cell Phone/Pager: " + model.CellPhone + "\n"
                     + "Fax: " + model.Fax + "\n"
                     + "Email: " + model.Email;

                SendMail(mailMessage);
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }
        }


        public static bool SendEmailTechnicalIssue(TechnicalModel model)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["emailGeneral"]);
                mailMessage.To.Add(new MailAddress(ConfigurationManager.AppSettings["TechSupport"]));
                mailMessage.Subject = "Technical Issue";

                mailMessage.Body = "First Name: " + model.FirstName + "\n"
                     + "Last Name: " + model.LastName + "\n"
                     + "Email: " + model.Email + "\n"
                     + "Issue: " + model.Issue;

                return SendMail(mailMessage);
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return false;
            }
        }


        public static bool SendRegistrationConfirmationEmailToUser(UserModel userModel)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["webmaster"]);
                mailMessage.To.Add(new MailAddress(userModel.Username));
                mailMessage.Subject = Languages.RegisterPartial.RegistrationEmailSubject;
                mailMessage.IsBodyHtml = true;


                string emailContent = Languages.RegisterPartial.RegistrationEmailToUserContent.Replace("{userName}", userModel.Username)
                    .Replace("{lastName}", userModel.LastName).Replace("{BaseURL}", ConfigurationManager.AppSettings["BaseURL"]).Replace("{password}", userModel.Password);

                if(userModel.UserType == Constants.UserRole.SC.ToString())
                {
                    emailContent = emailContent.Replace("{firstName}", userModel.FirstName);
                } else
                {
                    emailContent = emailContent.Replace("{firstName}", Languages.RegisterPartial.Dr);
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailContent, null, "text/html");
                mailMessage.AlternateViews.Add(htmlView);

                SendMail(mailMessage);

                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SendRegistrationConfirmationEmailToAdmin(UserModel userModel)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["webmaster"]);
                mailMessage.To.Add(new MailAddress(ConfigurationManager.AppSettings["CTUAdmin"]));
                mailMessage.Subject = Languages.RegisterPartial.RegistrationEmailSubject;

                string piInfo = "";
                string userFullName = "Dr. " + userModel.FirstName + " " + userModel.LastName;
                if (userModel.UserType.Equals(Constants.UserRole.SC.ToString()) || userModel.UserType.Equals(Constants.UserRole.SI.ToString()))
                {
                    UserRepository repo = new UserRepository();
                    InviteesMaster pi = repo.GetParentPI(userModel);

                    if (pi != null)
                    {
                        piInfo = "The PI for this user is " + "Dr. " + pi.FirstName + " " + pi.LastName + ".";
                    }
                }

                if (userModel.UserType == Constants.UserRole.SC.ToString())
                {
                    userFullName = userModel.FirstName + " " + userModel.LastName;
                }

                mailMessage.Body = userFullName + " has been added to the Goal International program as " + userModel.UserType + " with the username " + userModel.Username + ". "
                     + piInfo + "\n\n"
                     + "Username: " + userModel.Username + "\n"
                     + "Name: " + userFullName + "\n"
                     + "User Id: " + userModel.UserID + "\n"
                     + "Address: " + userModel.Address + "\n"
                     + "City: " + userModel.City + "\n"
                     + "Country: " + userModel.Country + "\n"
                     + "Language: " + userModel.Language;
                
                    

                SendMail(mailMessage);

                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }

        public static bool SendEmailToInvitee(InviteesMaster invitee, UserModel currentUser)
        {
            //UserModel currentUser = UserHelper.GetLoggedInUser();
            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["webmaster"]);
                mailMessage.To.Add(new MailAddress(invitee.Email));
                mailMessage.Subject = Languages.Support.EmailSubject;
                mailMessage.IsBodyHtml = true;

                string emailContent;
                if (invitee.UserType == Constants.UserRole.SI.ToString()) {
                    emailContent = Languages.Support.EmailContentToSI.Replace("{lastName}", invitee.LastName)
                        .Replace("{piLastName}", currentUser.LastName).Replace("{BaseURL}", ConfigurationManager.AppSettings["BaseURL"]).Replace("{password}", ConfigurationManager.AppSettings["password"])
                        .Replace("{institution}", currentUser.ClinicName).Replace("{email}", invitee.Email);
                }else
                {
                    emailContent = Languages.Support.EmailContentToSC.Replace("{lastName}", invitee.LastName)
                       .Replace("{piLastName}", currentUser.LastName).Replace("{BaseURL}", ConfigurationManager.AppSettings["BaseURL"]).Replace("{password}", ConfigurationManager.AppSettings["password"])
                       .Replace("{institution}", currentUser.ClinicName).Replace("{email}", invitee.Email).Replace("{firstName}", invitee.FirstName);
                }

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(emailContent, null, "text/html");
                mailMessage.AlternateViews.Add(htmlView);

                SendMail(mailMessage);

                return true;
            }

            catch (Exception e)
            {
                return false;
            }
        }


        public static void SendEmailAddPatient(UserModel user, int patientID)
        {

            try
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();

                mailMessage.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["emailGeneral"]);
                mailMessage.To.Add(new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["CTUAdmin"]));
                mailMessage.Subject = "Add New Patient";
                mailMessage.IsBodyHtml = true;

                UserRepository repo = new UserRepository();
                string piName = repo.GetPIFullName(user);
                string piCountry = repo.GetPICountry(user);
                string mailBody;
                // mailBody = "PI Dr. " + piName + " has entered a new patient. Patient ID: " + patientID + " in Country " + piCountry + ". Submitted on " + DateTime.Now.ToString("yyyy-MM-dd") + ".";

                string bodyPart = " in the country of " + piCountry + " has entered a new patient. Patient ID: " + patientID + ". Submitted on " + DateTime.Now.ToString("yyyy-MM-dd") + ".";
                if (user.UserType.Equals("SI"))
                {
                    mailBody = "SI " + user.FirstName + " " + user.LastName + " under PI Dr. " + piName + bodyPart;
                        
                }
                else //PI, SC
                {
                    mailBody = "PI Dr. " + piName + bodyPart;
                }
                

                
               
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody, null, "text/html");
                mailMessage.AlternateViews.Add(htmlView);

                UserHelper.SendMail(mailMessage);
            }

            catch (Exception e)
            {
                string msg = e.Message;
            }
        }

    }
}
