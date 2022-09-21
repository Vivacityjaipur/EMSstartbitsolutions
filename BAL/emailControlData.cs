    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using BOL;
using BOL.Responses;

namespace BAL
    {
    public class emailControlData : IemailControlData
    {
        public static string OTP = "";
        public static int forgetuserid = 0;
        private IloginData _lodata;

        private IemployeeData _ldata;
        public emailControlData(IemployeeData ldata, IloginData lodata)
        {
            _lodata = lodata;
            _ldata = ldata;
        }
        public async Task<statusResponse> CheckId(string id)
        {
            var isdigit = int.TryParse(id, out _);
            var otp = GeneratePassword().ToString();
            OTP = otp;
            var subject = "Ems forget password otp.";
            var body = "your otp for ems forget password is " + otp;
            if (id.Contains('@') && id.Contains('.'))
            {
                var user = await _ldata.GetByEmailId(id);
                if(user == null)
                {
                    return new statusResponse { Code=404, Message = "User Not Found" };
                }
                var useremail = user.officeemail;
                forgetuserid = user.employee_id;
                SendEmail(useremail, body, subject);
                return new statusResponse { Code = 200, Message = "OTP sent Successfully" };


            }
            else if (isdigit)
            {
                var user = await _ldata.GetById(Convert.ToInt32(id));
                if (user == null)
                {
                    return new statusResponse { Code = 404, Message = "User Not Found" };
                }
                var useremail = user.officeemail;
                forgetuserid = user.employee_id;
                SendEmail(useremail, body, subject);
                return new statusResponse { Code = 200, Message = "OTP sent Successfully" };
            }
            else
            {
                return new statusResponse { Code = 400, Message = "Entered Email/Id is not valid" };
            }
        }
        public async Task<statusResponse> ChangePassword(forget fo)
        {
            if (fo.otp == OTP)
            {

                if (fo.newpassword == fo.confirmpassword)
                {
                    var user = await _lodata.getByEid(forgetuserid);
                    if(user == null)
                    {
                        return new statusResponse { Code = 404, Message = "User Not Found" };
                    }
                    user.password = fo.newpassword;
                    await _lodata.Update(user);
                    return new statusResponse { Code = 200, Message = "Changed Successfully" };

                }
                else
                {
                    return new statusResponse { Code = 400, Message = "new password and confirm password not matched!" };

                }
            }
            else
            {
                return new statusResponse { Code = 400, Message = "Invalid OTP!" };
            }
        }
        private string GeneratePassword()
        {
            string PasswordLength = "6";
            string NewPassword = "";

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";


            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string IDString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;
            }
            return NewPassword;
        }
        public  statusResponse SendEmail(string emailAddress, string body, string subject)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("vikashmangal0@gmail.com");
                    mail.To.Add(emailAddress);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;


                    using (SmtpClient smtp = new SmtpClient("smtp-relay.sendinblue.com", 587))
                    {
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = new NetworkCredential("vikashmangal0@gmail.com", "mxBHZtbELRSjWU46");
                        smtp.Send(mail);
                    }
                }
                return new statusResponse { Code = 200, Message = "Mail sent Success fully" };

            }
            catch(Exception ex)
            {
                return new statusResponse { Code = 500, Message = ex.Message };
            }
        }
      
    }
}


