using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Email
{
    /// <summary>
    /// 邮件服务接口
    /// </summary>
    public interface IEmailEngine
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="account">邮件发送账号信息</param>
        /// <param name="options">邮件设置项</param>
        /// <returns>邮件发送结果</returns>
        Task<EmailResult> SendAsync(EmailAccount account, params EmailOption[] options);
    }
}
