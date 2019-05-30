using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Email
{
    /// <summary>
    /// 邮件发送管理
    /// </summary>
    public static class EmailSender
    {
        static EmailSender()
        {
            EmailEngine = new NetEmailEngine();
        }

        #region 属性

        /// <summary>
        /// 邮件发送执行器
        /// </summary>
        public static IEmailEngine EmailEngine { get; set; } = null;

        /// <summary>
        /// 获取或设置获取邮件的方法
        /// </summary>
        public static Func<EmailOption, List<EmailAccount>> GetEmailAccountMethod
        {
            get;
            set;
        }

        #endregion

        #region 发送方法

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="options">邮件选项信息</param>
        /// <returns>发送结果</returns>
        public static async Task<EmailResult> SendAsync(params EmailOption[] options)
        {
            if (EmailEngine == null)
            {
                throw new Exception("没有配置任何的邮件发送执行器");
            }
            Dictionary<string, List<EmailOption>> emailOptionGroup = new Dictionary<string, List<EmailOption>>();
            Dictionary<string, EmailAccount> accounts = new Dictionary<string, EmailAccount>();

            #region 获取服务器信息

            foreach (var option in options)
            {
                var emailAccounts = GetAccounts(option);
                foreach (var account in emailAccounts)
                {
                    string accountKey = account.IdentityKey;
                    if (accounts.ContainsKey(accountKey))
                    {
                        emailOptionGroup[accountKey].Add(option);
                    }
                    else
                    {
                        emailOptionGroup.Add(accountKey, new List<EmailOption>() { option });
                        accounts.Add(accountKey, account);
                    }
                }
            }

            #endregion

            EmailResult result = new EmailResult()
            {
                Message = "发送成功",
                Success = true
            };

            #region 执行

            foreach (var optionGroup in emailOptionGroup)
            {
                var account = accounts[optionGroup.Key];
                var exectResult = await EmailEngine.SendAsync(account, optionGroup.Value.ToArray()).ConfigureAwait(false);
                if (!exectResult.Success)
                {
                    result = exectResult;
                }
            }

            #endregion

            return result;
        }

        #endregion

        #region 获取邮件发送账号

        /// <summary>
        /// 获取邮件发送账号
        /// </summary>
        /// <param name="option">邮件选项</param>
        /// <returns></returns>
        static List<EmailAccount> GetAccounts(EmailOption option)
        {
            if (option == null)
            {
                return new List<EmailAccount>(0);
            }
            List<EmailAccount> accountList = GetEmailAccountMethod?.Invoke(option);
            if (accountList == null || accountList.Count <= 0)
            {
                throw new Exception("not special any log server");
            }
            return accountList;
        }

        #endregion
    }
}
