using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZNEW.Email
{
    /// <summary>
    /// 邮件发送设置项
    /// </summary>
    public class EmailOption
    {
        Encoding subjectEncoding = Encoding.UTF8;
        Encoding bodyEncoding = Encoding.UTF8;
        bool bodyIsHtml = true;

        #region 属性

        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public List<string> EmailAddress
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件主题编码，默认UTF-8
        /// </summary>
        public Encoding SubjectEncoding
        {
            get
            {
                return subjectEncoding;
            }
            set
            {
                subjectEncoding = value;
            }
        }

        /// <summary>
        /// 正文编码
        /// </summary>
        public Encoding BodyEncoding
        {
            get
            {
                return bodyEncoding;
            }
            set
            {
                bodyEncoding = value;
            }
        }

        /// <summary>
        /// 邮件内容是否为HTML，默认为True
        /// </summary>
        public bool BodyIsHtml
        {
            get
            {
                return bodyIsHtml;
            }
            set
            {
                bodyIsHtml = value;
            }
        }

        #endregion
    }
}
