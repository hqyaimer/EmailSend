using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Hqyaimer.Email;

namespace EmailTest
{
    class Program
    {
        /// <summary>
        /// 发送邮箱
        /// </summary>
        static string fromEmail = "your from email";
        /// <summary>
        /// 收件邮箱
        /// </summary>
        static string toEmail = "your to email";
        /// <summary>
        /// 发送授权码
        /// </summary>
        static string authorization = "your authorization number";
        /// <summary>
        /// 发送人邮箱类型
        /// your send mailbox type
        /// </summary>
        static EmailSend.EmailType sendtype = EmailSend.EmailType._新浪邮箱;

        static void Main(string[] args)
        {
            string title = "Hello World!";
            string content = "<div style=\"height:50px;width:100%;background:#333;color:#fff;text-align:center;line-height:50px;\">邮件Api发送测试, Hello_World!</div>";
            EmailSend es = new EmailSend(fromEmail, authorization, sendtype);
            es.ToEmailList.Add(toEmail);
            TestSend(es, title, content);
            Console.ReadLine();
        }

        public static void TestSend(EmailSend es, string toemail, string title, string content)
        {
            if (es.Send(toemail, title, content))
            {
                Console.WriteLine("发送成功");
            }
            else
            {
                Console.WriteLine("发送失败");
            }
        }

        public static void TestSend(EmailSend es, string title, string content)
        {
            if (es.Send(title, content))
            {
                Console.WriteLine("发送成功");
            }
            else
            {
                Console.WriteLine("发送失败");
            }
        }
    }
}
