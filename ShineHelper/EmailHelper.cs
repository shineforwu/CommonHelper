using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ShineHelper
{
    public class EmailHelper
    {
        public string Host { get; set; } = "smtp.163.com";
        public string UserName { get; set; }
        public string AliasName { get; set; }
        public string Password { get; set; } 
        public string Subject { get; set; } = "Subject";
        public string Body { get; set; } = "Body";

        public void RunTest()
        {
            SendMailUse("xxxxxx@163.com");
        }

        public  void SendMailUse(string sendTo, string copyTo="")
        {
           
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = Host;//邮件服务器
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(UserName, Password);//用户名、密码
            client.EnableSsl = true;
            client.EnableSsl = true;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(UserName, AliasName);
            msg.To.Add(sendTo);
            if(copyTo!="")
            {
                msg.CC.Add(copyTo);
            }
            
            msg.Subject = Subject;
            msg.Body = Body;  
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            msg.Priority = MailPriority.High;
            


            try
            {
                client.Send(msg);
                Console.WriteLine("Send OK");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message, "Send Fail ");
                throw (ex);
                
            }
        }
    }
}
