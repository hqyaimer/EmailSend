using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailTest
{
    /// <summary>
    /// 邮件发送类
    /// </summary>
    public class EmailSend
    {
        /// <summary>
        /// 创建邮件发送类
        /// 发送人授权码通常获取方式为：
        /// 进入邮箱设置界面 -> 找到POP3/SMTP/IMAP设置 -> 开启POP3/SMTP/IMAP服务 -> 附近可找到授权码(通常叫做客户端授权码)
        /// </summary>
        /// <param name="fromemail">发送人邮箱</param>
        /// <param name="toemail">接收人邮箱</param>
        /// <param name="authorization">发送人授权码</param>
        /// <param name="emailtype">发送人邮箱类型</param>
        public EmailSend(string fromemail, string authorization, EmailType emailtype)
        {
            this.fromemail = fromemail;
            this.authorization = authorization;
            this.emailtype = emailtype;
            switch (emailtype)
            {
                case EmailType._QQ个人邮箱:
                    host = "smtp.qq.com";
                    break;
                case EmailType._QQ企业邮箱:
                    host = "smtp.exmail.qq.com";
                    break;
                case EmailType._网易邮箱:
                    host = "smtp.163.com";
                    break;
                case EmailType._126邮箱:
                    host = "smtp.126.com";
                    break;
                case EmailType._新浪邮箱:
                    host = "smtp.sina.cn";
                    break;
                default:
                    host = null;
                    break;
            }
        }

        /// <summary>
        /// 邮件发送类型枚举
        /// </summary>
        public enum EmailType
        {
            _QQ个人邮箱,
            _QQ企业邮箱,
            _新浪邮箱,
            _网易邮箱,
            _126邮箱
        }

        #region 私有变量
        /// <summary>
        /// 发送人邮箱
        /// </summary>
        private string fromemail { get; set; }
        /// <summary>
        /// 发送人授权码
        /// </summary>
        private string authorization { get; set; }
        /// <summary>
        /// 发送人邮箱类型
        /// </summary>
        private EmailType emailtype { get; set; }
        /// <summary>
        /// 发送主机地址
        /// </summary>
        private string host { get; set; }
        #endregion

        #region 公有变量
        /// <summary>
        /// 收件人列表
        /// </summary>
        public List<string> ToEmailList = new List<string>();
        /// <summary>
        /// 邮件主题编码
        /// </summary>
        public Encoding SubjectEncoding = Encoding.UTF8;
        /// <summary>
        /// 内容编码
        /// </summary>
        public Encoding BodyEncoding = Encoding.Default;
        /// <summary>
        /// 邮件优先级
        /// </summary>
        public MailPriority Priority = MailPriority.High;
        /// <summary>
        /// 是否以Html格式发送
        /// </summary>
        public bool IsBodyHtml = true;
        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> Attachments = new List<Attachment>();
        #endregion

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string title,string content)
        {
            if (host == null || ToEmailList.Count() == 0)
            {
                return false;
            }
            try
            {
                //实例化一个发送邮件类
                MailMessage mailMessage = new MailMessage();
                //发件人邮箱地址，方法重载不同，可以根据需求自行选择
                mailMessage.From = new MailAddress(fromemail);
                //收件人列表
                foreach (string email in ToEmailList)
                {
                    mailMessage.To.Add(new MailAddress(email));
                }
                //邮件标题
                mailMessage.Subject = title;
                mailMessage.SubjectEncoding = SubjectEncoding;
                //邮件内容
                mailMessage.Body = content;
                mailMessage.BodyEncoding = BodyEncoding;
                //设置邮件优先级
                mailMessage.Priority = Priority;
                //是否以Html格式发送
                mailMessage.IsBodyHtml = IsBodyHtml;
                //添加附件
                if (Attachments.Count() > 0)
                {
                    foreach (Attachment atta in Attachments)
                    {
                        mailMessage.Attachments.Add(atta);
                    }
                }
                //实例化一个SmtpClient类
                SmtpClient client = new SmtpClient();
                client.Host = this.host;
                //使用安全加密连接
                client.EnableSsl = true;
                //不和请求一块发送
                client.UseDefaultCredentials = false;
                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码)
                client.Credentials = new NetworkCredential(fromemail, authorization);
                //发送
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 指定发送邮件
        /// </summary>
        /// <param name="toemail">指定邮箱</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string toemail,string title, string content)
        {
            if (host == null)
            {
                return false;
            }
            try
            {
                //实例化一个发送邮件类
                MailMessage mailMessage = new MailMessage();
                //发件人邮箱地址，方法重载不同，可以根据需求自行选择
                mailMessage.From = new MailAddress(fromemail);
                //收件人列表
                mailMessage.To.Add(new MailAddress(toemail));
                //邮件标题
                mailMessage.Subject = title;
                mailMessage.SubjectEncoding = SubjectEncoding;
                //邮件内容
                mailMessage.Body = content;
                mailMessage.BodyEncoding = BodyEncoding;
                //设置邮件优先级
                mailMessage.Priority = Priority;
                //是否以Html格式发送
                mailMessage.IsBodyHtml = IsBodyHtml;
                //添加附件
                if (Attachments.Count() > 0)
                {
                    foreach (Attachment atta in Attachments)
                    {
                        mailMessage.Attachments.Add(atta);
                    }
                }
                //实例化一个SmtpClient类
                SmtpClient client = new SmtpClient();
                client.Host = this.host;
                //使用安全加密连接
                client.EnableSsl = true;
                //不和请求一块发送
                client.UseDefaultCredentials = false;
                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码)
                client.Credentials = new NetworkCredential(fromemail, authorization);
                //发送
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 增加附件
        /// </summary>
        /// <param name="path">附件路径</param>
        public void AddNewAttachment(string path) {
            Attachment attach = new Attachment(path);
            this.Attachments.Add(attach);
        }
    }
}
