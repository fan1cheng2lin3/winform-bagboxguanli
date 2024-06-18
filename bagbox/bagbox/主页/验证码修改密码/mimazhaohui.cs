using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using bagbox.前端窗口;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Reflection.Emit;

namespace bagbox
{
    public partial class mimazhaohui : Form
    {

        public static string a = string.Empty;

        public mimazhaohui()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }

        private void mimazhaohui_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }

        public bool SendMail(string mailFrom, string mailTo, string token, string subject, string body)
        {

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Host = "smtp.qq.com";
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(mailFrom, token);
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Low;
            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public string GenerateRandomCode(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRecipientEmailFromDatabase(string name)
        {
            string email = "";
            string query = "SELECT Email FROM customer_Table WHERE Name = @Name";

            using (SqlConnection connection = DB.GetCn())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            email = reader["Email"].ToString();
                        }
                    }
                }
            }

            return email;
        }

        private Dictionary<string, string> verificationCodes = new Dictionary<string, string>();


        public void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";

            if (!Regex.IsMatch(txtPwd.Text, pattern))
            {
                txtPwd.Clear();
            }
            if (txtName.Text == "" || txtPwd.Text == "")
            {
                label4.Visible = true;
            }
            else
            {
                label4.Visible = false;               
                string query = "SELECT * FROM customer_Table WHERE Name = @Name AND Email = @Email";
                SqlCommand cmd = new SqlCommand(query, DB.GetCn());
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Email", txtPwd.Text);
                DataTable dt = DB.GetDataSet(cmd);

                if (dt.Rows.Count > 0)
                {
                    DB.GetCn();
                    // 检查上次发送验证码的时间
                    object lastCaptchaSentTimeObj = dt.Rows[0]["last_captcha_sent_time"];
                    DateTime lastCaptchaSentTime;
                    if (lastCaptchaSentTimeObj != DBNull.Value)
                    {
                        lastCaptchaSentTime = Convert.ToDateTime(lastCaptchaSentTimeObj);
                        if (DateTime.Now.Subtract(lastCaptchaSentTime).TotalHours > 99999)
                        //if (DateTime.Now.Subtract(lastCaptchaSentTime).TotalHours < 1)
                        {
                            MessageBox.Show("1 小时内只能发送一次验证码，请稍后重试。");
                            return;
                        }
                    }

                    string verificationCode = GenerateRandomCode(6);
                    string updateQuery = $"UPDATE customer_Table SET captcha = '{verificationCode}', last_captcha_sent_time = GETDATE() WHERE Name = '{txtName.Text}'";
                    bool success = DB.sqlEx(updateQuery);

                    if (success)
                    {
                        string token = ConfigurationManager.AppSettings["token"];
                        string userName = txtName.Text;
                        string mailTo = GetRecipientEmailFromDatabase(userName);
                        string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
                        SendMail(mailFrom, mailTo, token, "验证码", $"您的验证码是：{verificationCode},5分钟后失效");
                        a = txtName.Text;
                        MessageBox.Show("验证码已发送，请检查您的邮箱。");
                        yanzhengma t1 = new yanzhengma();
                        t1.ShowDialog();
                        
                    }
                    else
                    {
                        MessageBox.Show("发送验证码失败");
                    }
                }
                else
                {
                    MessageBox.Show("用户名或者邮箱错误");
                }
            }


        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
