using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendTestEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtEmailFrom.Text = "promo@nickelscabinets.com";
            txtSMTP.Text = "mail.nickelscabinets.com";
            txtPassword.Text = "Nickels6760!";
            cbEmailSeguro.Checked = true;
            txtEmailTo.Text = "ailtonemiliojr@gmail.com";
            txtTitulo.Text = "Teste envio email";
            txtMensagem.Text = "Testando";
            txtPort.Text = "465";
        }

        private void SendEmail(string EmailTo, string EmailFrom, string Password, int Port, string SMTP, string Title, string MSG, bool emailseguro)
        {
            string Html = "";
            
            ServicePointManager.ServerCertificateValidationCallback = (obj, certificate, chain, errors) => true;

            MailMessage mail = new MailMessage();
            mail.Subject = Title;
            mail.From = new MailAddress(EmailFrom);
            mail.To.Add(EmailTo);

           
            Html = "<html>" +
                        "<body>" +
                            "<div>" + Title + "</div><br>" +
                            "<div>" + MSG + "</div><br>" +
                            "   <div> This is an auto email, please do not reply. </div><br><br> " +
                        "</body>" +
                    "</html>";


            mail.Body = Html;

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient(SMTP, Port);
            smtp.EnableSsl = emailseguro;
            NetworkCredential netCre = new NetworkCredential(EmailFrom,Password);
            smtp.Credentials = netCre;

            try
            {
                smtp.Send(mail);
                smtp.Dispose();
                MessageBox.Show("Email enviado com successo !");
            }
            catch (Exception ex)
            {
                string msgError = ex.Message.ToString();

                MessageBox.Show(msgError);

                smtp.Dispose();
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            SendEmail(txtEmailTo.Text, txtEmailFrom.Text, txtPassword.Text, Convert.ToInt32(txtPort.Text), txtSMTP.Text, txtTitulo.Text, txtMensagem.Text, cbEmailSeguro.Checked);
        }
    }
}
