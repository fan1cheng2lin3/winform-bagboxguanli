using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxSystemMange
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";

            // 检查用户名是否存在
            string query = "SELECT * FROM customer_Table WHERE Name = @Name";
            SqlCommand cmd2 = new SqlCommand(query, DB.GetCn());
            cmd2.Parameters.AddWithValue("@Name", txtName.Text);
            DataTable dt = DB.GetDataSet(cmd2);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("用户名不存在，请重新输入");
                return;
            }

            // 检查密码和邮箱格式
            if (Regex.IsMatch(txtPwd.Text, @"[\u4e00-\u9fa5]") || txtPwd.Text.Length < 6)
            {
                txtPwd.Clear();
                MessageBox.Show("密码不能包含中文且必须6位以上");
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text, pattern))
            {
                txtEmail.Clear();
                MessageBox.Show("邮箱格式错误，请输入正确的邮箱");
                return;
            }

            // 检查所有字段是否为空
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPwd.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(textCaptcha.Text))
            {
                MessageBox.Show("请输入账号、密码、邮箱和验证码");
                return;
            }

            // 检查验证码是否正确
            string query2 = "SELECT * FROM customer_Table WHERE Name = @Name AND Captcha = @Captcha";
            SqlCommand cmd6 = new SqlCommand(query2, DB.GetCn());
            cmd6.Parameters.AddWithValue("@Name", txtName.Text);
            cmd6.Parameters.AddWithValue("@Captcha", textCaptcha.Text);
            DataTable dt2 = DB.GetDataSet(cmd6);
            if (dt2.Rows.Count == 0)
            {
                MessageBox.Show("验证码错误");
                return;
            }

            // 更新用户密码
            string hashedPassword = DB.PasswordHasher.HashPassword(txtPwd.Text);
            string updateQuery = "UPDATE customer_Table SET Password = @Password WHERE Name = @Name";
            SqlCommand cmd = new SqlCommand(updateQuery, DB.GetCn());
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);
            DB.GetDataSet(cmd);

            //MessageBox.Show("密码修改成功");

            //// 显示密码找回对话框并关闭当前窗口
            //mimazhaohui t1 = new mimazhaohui();
            //t1.ShowDialog();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPwd.PasswordChar = '\0';
            }
            else
            {
                txtPwd.PasswordChar = '*';
            }
        }

        private void Signup_Load(object sender, EventArgs e)
        {
            this.AcceptButton = button1;
        }
      
      
        private void button2_Click_1(object sender, EventArgs e)
        {

            

            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";

            // 检查用户名是否存在
            string query = "SELECT * FROM customer_Table WHERE Name = @Name";
            SqlCommand cmd2 = new SqlCommand(query, DB.GetCn());
            cmd2.Parameters.AddWithValue("@Name", txtName.Text);
            DataTable dt = DB.GetDataSet(cmd2);
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("此用户名已存在，请重新输入");
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text, pattern))
            {
                txtEmail.Clear();
                MessageBox.Show("邮箱格式错误，请输入正确的邮箱");
                return;
            }

            // 检查所有字段是否为空
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("请输入账号、邮箱和验证码");
                return;
            }

            string sdr = @"
            INSERT INTO customer_Table (Name, Email) VALUES 
            (@Name, @Email);";

            SqlCommand cmd = new SqlCommand(sdr, DB.GetCn());
            cmd.Parameters.AddWithValue("@Name", txtName.Text);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            DB.GetDataSet(cmd);

            string query4 = "SELECT * FROM customer_Table WHERE Name = @Name AND Email = @Email";
            SqlCommand cmd5 = new SqlCommand(query4, DB.GetCn());
            cmd5.Parameters.AddWithValue("@Name", txtName.Text);
            cmd5.Parameters.AddWithValue("@Email", txtEmail.Text);
            DataTable dt5 = DB.GetDataSet(cmd5);

            if (dt5.Rows.Count > 0)
            {

                DB.GetCn();
                // 检查上次发送验证码的时间
                object lastCaptchaSentTimeObj = dt5.Rows[0]["last_captcha_sent_time"];
                DateTime lastCaptchaSentTime;
                if (lastCaptchaSentTimeObj != DBNull.Value)
                {
                    lastCaptchaSentTime = Convert.ToDateTime(lastCaptchaSentTimeObj);
                    if (DateTime.Now.Subtract(lastCaptchaSentTime).TotalHours < 1)
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
                    string mailFrom = ConfigurationManager.AppSettings["mailFrom"];
                    SendMail(mailFrom, txtEmail.Text, token, "验证码", $"您的验证码是：{verificationCode},5分钟后失效");
                    MessageBox.Show("验证码已发送，请检查您的邮箱。");
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
    }
}
