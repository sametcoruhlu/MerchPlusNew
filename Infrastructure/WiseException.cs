using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Mail;

namespace Infrastructure
{
    public class WiseException : Exception
    {

        public override string Message
        {
            get
            {
                return this.msgToUser + "\r\n\r\n" + this.msg;
            }
        }
        private string msg;
        private string msgToUser;
        public WiseException(string messageToUser, string message)
            : base(messageToUser)
        {
            msg = message;
            msgToUser = messageToUser;
            WriteExceptionToDb();
            //WriteEventLog();
            //SendEmail();
        }
        public WiseException(string messageToUser)
            : base(messageToUser)
        {
            msgToUser = messageToUser;
            WriteExceptionToDb();
            WriteEventLog();
        }
        private void WriteExceptionToDb()
        {
            //try
            //{
            //    SqlConnection insSqlConnection = new SqlConnection(InfrastructureLayer.ConStr.DbConStr);
            //    SqlCommand insSqlCommand = new SqlCommand();
            //    insSqlCommand.Connection = insSqlConnection;
            //    insSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //    insSqlCommand.CommandText = "AddExceptions";

            //    SqlParameter insSqlParameterMessage = new SqlParameter();

            //    insSqlParameterMessage.ParameterName = "@pMessage";
            //    insSqlParameterMessage.Value = this.msg;
            //    insSqlCommand.Parameters.Add(insSqlParameterMessage);

            //    SqlParameter insSqlParameterSource = new SqlParameter();
            //    insSqlParameterSource.ParameterName = "@pSource";
            //    if (this.Source == null)
            //    {
            //        insSqlParameterSource.Value = DBNull.Value;
            //    }
            //    else
            //    {
            //        insSqlParameterSource.Value = this.Source;
            //    }
            //    insSqlCommand.Parameters.Add(insSqlParameterSource);


            //    SqlParameter insSqlParameterStackTrace = new SqlParameter();
            //    insSqlParameterStackTrace.ParameterName = "@pStackTrace";
            //    if (this.StackTrace == null)
            //    {
            //        insSqlParameterStackTrace.Value = DBNull.Value;
            //    }
            //    else
            //    {
            //        insSqlParameterStackTrace.Value = this.StackTrace;
            //    }
            //    insSqlCommand.Parameters.Add(insSqlParameterStackTrace);

            //    SqlParameter insSqlParameterExceptionDate = new SqlParameter();
            //    insSqlParameterExceptionDate.ParameterName = "@pExceptionDate";

            //    insSqlParameterExceptionDate.Value = DateTime.Now;
            //    insSqlCommand.Parameters.Add(insSqlParameterExceptionDate);

            //    SqlParameter insSqlParameterEKullaniciId = new SqlParameter();
            //    insSqlParameterEKullaniciId.ParameterName = "@pKullaniciId";
            //    if (User.KullaniciId == 0)
            //    {
            //        insSqlParameterEKullaniciId.Value = DBNull.Value;
            //    }
            //    else
            //    {
            //        insSqlParameterEKullaniciId.Value = User.KullaniciId;
            //    }
            //    insSqlCommand.Parameters.Add(insSqlParameterEKullaniciId);

            //    SqlParameter insSqlParameterExceptionId = new SqlParameter();
            //    insSqlParameterExceptionId.ParameterName = "@pExceptionId";
            //    insSqlParameterExceptionId.Direction = System.Data.ParameterDirection.Output;
            //    insSqlParameterExceptionId.DbType = System.Data.DbType.Int32;
            //    insSqlCommand.Parameters.Add(insSqlParameterExceptionId);

            //    insSqlConnection.Open();
            //    insSqlCommand.ExecuteNonQuery();
            //    insSqlConnection.Close();
            //    mesajId = insSqlParameterExceptionId.Value.ToString();
            //    this.msgToUser += " Hata Kodu : " + insSqlParameterExceptionId.Value.ToString();
            //}
            //catch (Exception ex)
            //{
            //    WriteEventLog();
            //}
        }
        private void SendEmail()
        {
            //if (System.Configuration.ConfigurationManager.AppSettings["sendExceptionsAsMail"].ToString() == "1")
            //{
            //    try
            //    {
            //        Crypto insCrypto = new Crypto();
            //        SmtpClient scli = new SmtpClient(insCrypto.DecryptString(System.Configuration.ConfigurationManager.AppSettings["smtpServer"].ToString()));
            //        if (bool.Parse(insCrypto.DecryptString(System.Configuration.ConfigurationManager.AppSettings["SMTPAUTHENTICATE"])))
            //        {
            //            scli.Credentials = new System.Net.NetworkCredential(insCrypto.DecryptString(System.Configuration.ConfigurationManager.AppSettings["SMTPUSER"]), insCrypto.DecryptString(System.Configuration.ConfigurationManager.AppSettings["SMTPPASSWORD"]));
            //        }
            //        string targetSite = "";
            //        if (this.TargetSite != null)
            //        {
            //            targetSite = this.TargetSite.ToString();
            //        }
            //        string stackTrace = "";
            //        if (this.StackTrace != null)
            //        {
            //            stackTrace = this.StackTrace.ToString();
            //        }
            //        string strMailMain = "Mesaj No: " + mesajId + "<br>Mesaj : " + this.msg + "<br>Detail : " + this.ToString() + "<br>Target Site : " + targetSite + "<br>Source : " + this.Source + "<br>Tarih : " + busMETS.BusBase.GetServerDateTime().ToString() + "<br>Kullanici : " + User.KullaniciId.ToString();
            //        strMailMain += "<BR> Program Versiyonu: " + User.ProgramVersiyonu + "<BR> Makina Adı :" + System.Environment.MachineName + " <BR> Domain Kullanıcı Adı :" + System.Environment.UserDomainName;
            //        MailMessage mess = new MailMessage();
            //        mess.From = new MailAddress(insCrypto.DecryptString(System.Configuration.ConfigurationManager.AppSettings["from"].ToString()));
            //        foreach (string str in insCrypto.DecryptString(System.Configuration.ConfigurationManager.AppSettings["to"].ToString()).Split("|".ToCharArray()))
            //        {

            //            mess.To.Add(new MailAddress(str));

            //        }
            //        foreach (string str in insCrypto.DecryptString(System.Configuration.ConfigurationManager.AppSettings["cc"].ToString()).Split("|".ToCharArray()))
            //        {

            //            mess.CC.Add(new MailAddress(str));

            //        }
            //        mess.Subject = "Mavi Kurye Hata";
            //        mess.Body = strMailMain;
            //        mess.IsBodyHtml = true;
            //        scli.Send(mess);
            //    }
            //    catch (Exception)
            //    {


            //    }
            //}

        }
        private void WriteEventLog()
        {
            //if (!EventLog.Exists("MaviKurye"))
            //{
            //    EventSourceCreationData escd = new EventSourceCreationData("MaviKurye", "MaviKurye");
            //    escd.MachineName = Environment.MachineName;
            //    EventLog.CreateEventSource(escd);
            //}
            //EventLog.WriteEntry("MaviKurye", this.msg);
        }
    }
    public class WiseBusinessException : WiseException
    {
        //public WiseBusinessException():base()
        //{

        //}
        public WiseBusinessException(string messageToUser, string message)
            : base(messageToUser, message)
        {

        }
        public WiseBusinessException(string messageToUser)
            : base(messageToUser)
        {

        }
    }

    public class WiseSqlException : WiseException
    {
        //public WiseBusinessException():base()
        //{

        //}
        public WiseSqlException(string messageToUser, string message)
            : base(messageToUser, message)
        {

        }
        public WiseSqlException(string messageToUser)
            : base(messageToUser)
        {

        }
    }

    public class WiseIOException : WiseException
    {
        //public WiseBusinessException():base()
        //{

        //}
        public WiseIOException(string messageToUser, string message)
            : base(messageToUser, message)
        {

        }
        public WiseIOException(string messageToUser)
            : base(messageToUser)
        {

        }
    }
}
