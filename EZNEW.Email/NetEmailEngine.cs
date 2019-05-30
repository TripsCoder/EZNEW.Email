using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Email
{
    /// <summary>
    /// 邮件发送服务实现
    /// </summary>
    public class NetEmailEngine : IEmailEngine
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="account">邮件发送账号信息</param>
        /// <param name="options">邮件设置项</param>
        /// <returns>邮件发送结果</returns>
        public async Task<EmailResult> SendAsync(EmailAccount account, params EmailOption[] options)
        {
            if (account == null || options == null)
            {
                return new EmailResult()
                {
                    Success = false,
                    Message = "没有只能任何邮件发送"
                };
            }
            foreach (var option in options)
            {
                string[] toEmailAddress = option.EmailAddress.Where(email => !string.IsNullOrWhiteSpace(email)).ToArray();//清除不规则的邮件地址
                if (toEmailAddress == null || toEmailAddress.Length == 0)
                {
                    continue;
                }
                MailAddress fromMailAddress = new MailAddress(account.EmailAddress, account.SendPersonName);//发送者
                MailMessage nowMessage = new MailMessage();//当前要发送的邮件消息对象
                nowMessage.Sender = fromMailAddress;//设置发送者
                nowMessage.From = fromMailAddress;//设置发送者
                foreach (string email in toEmailAddress)
                {
                    nowMessage.To.Add(new MailAddress(email));//添加收件人
                }
                nowMessage.Subject = option.Subject;//邮件主题
                nowMessage.SubjectEncoding = option.SubjectEncoding;//标题编码
                nowMessage.Body = option.Content;//邮件正文
                nowMessage.BodyEncoding = option.BodyEncoding;//邮件正文编码
                nowMessage.IsBodyHtml = option.BodyIsHtml;
                SmtpClient client = new SmtpClient();//SMTP服务器
                client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定邮件发送方式
                client.EnableSsl = account.EnableSsl;
                client.Host = account.SMTP;//设置SMTP服务器地址
                client.Port = int.Parse(account.Port);//端口号
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(account.UserName, account.Pwd);//设置登录凭据
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                await client.SendMailAsync(nowMessage).ConfigureAwait(false);
            }
            return new EmailResult()
            {
                Success = true,
                Message = "邮件发送成功"
            };
        }
    }
}
