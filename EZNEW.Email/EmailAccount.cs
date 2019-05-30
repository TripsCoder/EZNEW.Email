using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Email
{
    /// <summary>
    /// 邮件账户
    /// </summary>
    public class EmailAccount
    {
        #region 属性

        /// <summary>
        /// 获取或设置发件人姓名
        /// </summary>
        public string SendPersonName
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置网站邮箱地址
        /// </summary>
        public string EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;set;
        }

        /// <summary>
        /// 获取或设置SMTP服务器地址
        /// </summary>
        public string SMTP
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置邮箱登陆密码
        /// </summary>
        public string Pwd
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置端口号
        /// </summary>
        public string Port
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置是否使用安全套接字连接
        /// </summary>
        public bool EnableSsl
        {
            get;
            set;
        }

        /// <summary>
        /// 账号标识
        /// </summary>
        public string IdentityKey
        {
            get
            {
                return GetIdentityKey();
            }
        }

        #endregion

        /// <summary>
        /// 获取服务器标识
        /// </summary>
        string GetIdentityKey()
        {
            return string.Format("{0}_{1}", SMTP, EmailAddress);
        }
    }
}
