using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml;
using BizPortal;
using BizPortalAdminWeb.Util;
using CIMB.Adapter;
using iSabaya;
using NHibernate.Context;

namespace WebHelper.Util
{
    public class Manage
    {
        public int ID { get; set; }

        public string Detail { get; set; }

        public string LargeImage { get; set; }

        public string Link { get; set; }

        public static string subDetail(string detail, int langth)
        {
            string subDetail = detail.Trim();
            for (int k = langth; k >= 0; k--)
            {
                if (detail[k].ToString().Equals(" "))
                {
                    subDetail = detail.Substring(0, k);
                    break;
                }
            }
            return subDetail;
        }

        private static IList<News> readNewXmlToObject()
        {
            IList<News> news = new List<News>();
            using (XmlReader reader = XmlReader.Create(ConfigurationManager.AppSettings["News"]))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "Item":
                                if (reader["ID"] != null)
                                {
                                    news.Add(new News
                                    {
                                        ID = Int32.Parse(reader["ID"]),
                                        Title = reader["Title"],
                                        Detail = reader["Detail"],
                                        Link = reader["Link"],
                                        Maker = reader["Maker"],
                                        MakerDateTime = reader["MakerDateTime"]
                                    });
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            return news;
        }

        public static IList<BizPortal.News> CreateHtml(int lengthTitle, int lenghtDetail, BizPortal.BizPortalSessionContext context)
        {
            IList<BizPortal.News> news = BizPortal.News.GetAll(context);
            foreach (BizPortal.News item in news)
            {
                if (item.EffectivePeriod.IsEffective())
                {
                    if (item.CreateAction != null)
                    {
                        if (item.Title.Length > lengthTitle && item.NewsBrief.Length > lenghtDetail)
                            item.Preview =
                                String.Format(
                                    "<span style='color:#990000;font-weight:bold'>{0}</span> {1}<br/><span style='color:#B2B2B2;font-size:10px;'>{2}</span><br/>" +
                                    "<a target='_blank' style='text-decoration:none;text-decoration: blink;font-size:12px;' href='{3}'><font color='red'>อ่านเพิ่มเติม>></font></a><br/><hr/>",
                                    subDetail(item.Title, lengthTitle), subDetail(item.NewsBrief, lenghtDetail),
                                    item.CreateAction.Timestamp, item.URL);
                        else if (item.Title.Length < lengthTitle && item.NewsBrief.Length > lenghtDetail)
                            item.Preview =
                                String.Format(
                                    "<span style='color:#990000;font-weight:bold'>{0}</span> {1}<br/><span style='color:#B2B2B2;font-size:10px;'>{2}</span><br/>" +
                                    "<a target='_blank' style='text-decoration:none;text-decoration: blink;font-size:12px;' href='{3}'><font color='red'>อ่านเพิ่มเติม>></font></a><br/><hr/>",
                                    item.Title, subDetail(item.NewsBrief, lenghtDetail), item.CreateAction.Timestamp,
                                    item.URL);
                        else if (item.NewsBrief.Length < lenghtDetail && item.Title.Length > lengthTitle)
                            item.Preview =
                                String.Format(
                                    "<span style='color:#990000;font-weight:bold'>{0}</span> {1}<br/><span style='color:#B2B2B2;font-size:10px;'>{2}</span><br/>" +
                                    "<a target='_blank' style='text-decoration:none;text-decoration: blink;font-size:12px;' href='{3}'><font color='red'>อ่านเพิ่มเติม>></font></a><br/><hr/>",
                                    subDetail(item.Title, lengthTitle), item.NewsBrief, item.CreateAction.Timestamp,
                                    item.URL);
                        else if (item.Title.Length < lengthTitle && item.NewsBrief.Length < lenghtDetail)
                            item.Preview =
                                String.Format(
                                    "<span style='color:#990000;font-weight:bold'>{0}</span> {1}<br/><span style='color:#B2B2B2;font-size:10px;'>{2}</span><br/>" +
                                    "<a target='_blank' style='text-decoration:none;text-decoration: blink;font-size:12px;' href='{3}'><font color='red'>อ่านเพิ่มเติม>></font></a><br/><hr/>",
                                    item.Title, item.NewsBrief, item.CreateAction.Timestamp, item.URL);
                    }
                    else
                    {
                        if (item.Title.Length > lengthTitle && item.NewsBrief.Length > lenghtDetail)
                            item.Preview =
                                String.Format(
                                    "<span style='color:#990000;font-weight:bold'>{0}</span> {1}<br/><span style='color:#B2B2B2;font-size:10px;'>{2}</span><br/>" +
                                    "<a target='_blank' style='text-decoration:none;text-decoration: blink;font-size:12px;' href='{3}'><font color='red'>อ่านเพิ่มเติม>></font></a><br/><hr/>",
                                    subDetail(item.Title, lengthTitle), subDetail(item.NewsBrief, lenghtDetail),
                                    item.EffectivePeriod.EffectiveDate, item.URL);
                        else if (item.Title.Length < lengthTitle && item.NewsBrief.Length > lenghtDetail)
                            item.Preview =
                                String.Format(
                                    "<span style='color:#990000;font-weight:bold'>{0}</span> {1}<br/><span style='color:#B2B2B2;font-size:10px;'>{2}</span><br/>" +
                                    "<a target='_blank' style='text-decoration:none;text-decoration: blink;font-size:12px;' href='{3}'><font color='red'>อ่านเพิ่มเติม>></font></a><br/><hr/>",
                                    item.Title, subDetail(item.NewsBrief, lenghtDetail), item.EffectivePeriod.EffectiveDate,
                                    item.URL);
                        else if (item.NewsBrief.Length < lenghtDetail && item.Title.Length > lengthTitle)
                            item.Preview =
                                String.Format(
                                    "<span style='color:#990000;font-weight:bold'>{0}</span> {1}<br/><span style='color:#B2B2B2;font-size:10px;'>{2}</span><br/>" +
                                    "<a target='_blank' style='text-decoration:none;text-decoration: blink;font-size:12px;' href='{3}'><font color='red'>อ่านเพิ่มเติม>></font></a><br/><hr/>",
                                    subDetail(item.Title, lengthTitle), item.NewsBrief, item.EffectivePeriod.EffectiveDate,
                                    item.URL);
                        else if (item.Title.Length < lengthTitle && item.NewsBrief.Length < lenghtDetail)
                            item.Preview =
                                String.Format(
                                    "<span style='color:#990000;font-weight:bold'>{0}</span> {1}<br/><span style='color:#B2B2B2;font-size:10px;'>{2}</span><br/>" +
                                    "<a target='_blank' style='text-decoration:none;text-decoration: blink;font-size:12px;' href='{3}'><font color='red'>อ่านเพิ่มเติม>></font></a><br/><hr/>",
                                    item.Title, item.NewsBrief, item.EffectivePeriod.EffectiveDate, item.URL);
                    }
                }
                else
                {
                    item.Preview = "";
                }
            }
            return news;
        }

        //News
        private static IList<string> imagepath = null;

        public static IList<Manage> readXmlToObject()
        {
            List<Manage> lAds = new List<Manage>();
            imagepath = new List<string>();
            using (XmlReader reader = XmlReader.Create(ConfigurationManager.AppSettings["Ads"]))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "Item":
                                if (reader["ID"] != null || reader["Detail"] != null || reader["LargeImage"] != null)
                                {
                                    imagepath.Add(reader["LargeImage"]);
                                    lAds.Add(new Manage
                                    {
                                        ID = Int32.Parse(reader["ID"]),
                                        Detail = reader["Detail"],
                                        LargeImage = reader["LargeImage"],
                                        Link = reader["Link"]
                                    });
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            return lAds;
        }

        //Advertise
        public static IList<Advertise> readAdverTiseFromXML()
        {
            IList<Advertise> lAds = new List<Advertise>();
            imagepath = new List<string>();
            using (XmlReader reader = XmlReader.Create(ConfigurationManager.AppSettings["Ads"]))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "Item":
                                if (reader["ID"] != null || reader["Detail"] != null || reader["LargeImage"] != null)
                                {
                                    imagepath.Add(reader["LargeImage"]);
                                    lAds.Add(new Advertise
                                    {
                                        ID = Int32.Parse(reader["ID"]),
                                        Detail = reader["Detail"],
                                        LargeImage = reader["LargeImage"],
                                        Link = reader["Link"],
                                        StartDate = Manage.convertStringToDateTime(reader["StartDate"]),
                                        EndDate = Manage.convertStringToDateTime(reader["EndDate"]),
                                    });
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            return lAds;
        }

        public static IList<Advertise> readAdverTiseFromXMLClientWeb()
        {
            IList<Advertise> lAds = new List<Advertise>();
            imagepath = new List<string>();
            using (XmlReader reader = XmlReader.Create(ConfigurationManager.AppSettings["Ads"]))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        // Get element name and switch on it.
                        switch (reader.Name)
                        {
                            case "Item":
                                if (reader["ID"] != null || reader["Detail"] != null || reader["LargeImage"] != null)
                                {
                                    imagepath.Add(subImageOnly(reader["LargeImage"]));

                                    //imagepath.Add((reader["LargeImage"]));

                                    lAds.Add(new Advertise
                                    {
                                        ID = Int32.Parse(reader["ID"]),
                                        Detail = reader["Detail"],
                                        LargeImage = subImageOnly(reader["LargeImage"]),
                                        Link = reader["Link"],
                                        StartDate = Manage.convertStringToDateTime(reader["StartDate"]),
                                        EndDate = Manage.convertStringToDateTime(reader["EndDate"]),
                                    });
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            return lAds;
        }

        private static string subImageOnly(string imageOrignal)
        {
            string[] image = imageOrignal.Split('/');
            string root = HttpContext.Current.Server.MapPath("/BizPortalAdminWeb");
            string path = "\\Images\\Ads\\" + image[image.Length - 1];
            string directory = root + path;

            //return HttpContext.Current.Server.MapPath("~/Images/Ads/") + image[image.Length - 1];
            //return HttpContext.Current.Request.PhysicalApplicationPath + "Images\\Ads\\" + image[image.Length - 1];
            return directory;
        }

        public static void WriteAdvertiseToXML(IList<Advertise> adss)
        {
            using (XmlWriter write = XmlWriter.Create(ConfigurationManager.AppSettings["Ads"]))
            {
                write.WriteStartDocument();
                write.WriteStartElement("Ads");
                foreach (Advertise item in adss)
                {
                    write.WriteStartElement("Item");

                    write.WriteAttributeString("ID", item.ID.ToString());
                    write.WriteAttributeString("Detail", item.Detail);
                    write.WriteAttributeString("LargeImage", item.LargeImage);
                    write.WriteAttributeString("Link", item.Link);
                    write.WriteAttributeString("StartDate", item.StartDate.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")));
                    write.WriteAttributeString("EndDate", item.EndDate.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")));

                    write.WriteEndElement();
                }
                write.WriteEndElement();
                write.WriteEndDocument();
            }
        }

        // Methods Manage Image

        #region Image manage

        public static byte[] Inscribe(Image image, int size)
        {
            return Inscribe(image, size, size);
        }

        public static byte[] Inscribe(Image image, int width, int height)
        {
            //System.Drawing.Image oThumbNail = new Bitmap(image, width, height);
            ImageFormat imgfmt;
            if (ImageFormat.Jpeg.Equals(image.RawFormat))
                imgfmt = ImageFormat.Jpeg;
            else if (ImageFormat.Png.Equals(image.RawFormat))
                imgfmt = ImageFormat.Png;
            else if (ImageFormat.Gif.Equals(image.RawFormat))
                imgfmt = ImageFormat.Gif;
            else if (ImageFormat.Bmp.Equals(image.RawFormat))
                imgfmt = ImageFormat.Bmp;
            else
                throw new Exception("The image was invalid type.");

            var ms = new MemoryStream();
            using (System.Drawing.Image oThumbNail = new Bitmap(image, width, height))
            {
                using (Graphics oGraphic = Graphics.FromImage(oThumbNail))
                {
                    oGraphic.CompositingQuality = CompositingQuality.HighQuality;
                    oGraphic.SmoothingMode = SmoothingMode.HighQuality;
                    oGraphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    oGraphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Rectangle oRectangle = new Rectangle(0, 0, width, height);
                    oGraphic.DrawImage(image, oRectangle);
                }
                oThumbNail.Save(ms, imgfmt);
            }

            return ms.GetBuffer();
        }

        private static void SmoothGraphics(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        public static void SaveToJpeg(Image image, Stream output)
        {
            image.Save(output, ImageFormat.Jpeg);
        }

        public static void SaveToJpeg(Image image, string fileName)
        {
            image.Save(fileName, ImageFormat.Jpeg);
        }

        public static void SaveToType(Image image, string fileName, ImageFormat type)
        {
            image.Save(fileName, type);
        }

        public static void SaveToGif(Image image, string fileName)
        {
            image.Save(fileName, ImageFormat.Gif);
        }

        public static void SaveToGif(Image image, Stream output)
        {
            image.Save(output, ImageFormat.Gif);
        }

        public static void SaveToPng(Image image, string fileName)
        {
            image.Save(fileName, ImageFormat.Png);
        }

        public static void SaveToBmp(Image image, string fileName)
        {
            image.Save(fileName, ImageFormat.Bmp);
        }

        #endregion Image manage

        /*
         * Method Convert string date to DateTime
         * @param string ddmmyyyy
         * @return DateTime Object
         */

        public static DateTime convertStringToDateTime(string date)
        {
            try
            {
                string[] dateArray = date.Split('/');
                return new DateTime(Int32.Parse(dateArray[2]), Int32.Parse(dateArray[1]), Int32.Parse(dateArray[0]));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*
         * Method Compare Date
         */

        public static IList<Advertise> compareDate(IList<Advertise> ads)
        {
            DateTime Now = DateTime.Now;
            IList<Advertise> tempAds = new List<Advertise>();
            foreach (Advertise item in ads)
            {
                // StartDate and EndDate of Advertise if Expire load default logoCimb image
                if (!(item.StartDate <= Now && item.EndDate >= Now))
                {
                    item.LargeImage = "~/Images/Ads/logoCimb.jpg";
                }
                tempAds.Add(item);
            }
            return tempAds;
        }

        //Help

        #region Method Help other class

        public static string ObjectIsNullString(Object obj)
        {
            return obj != null ? obj.ToString() : "-";
        }

        #endregion Method Help other class

        //Approver
        /*
         * Method Send Email
         * @param IList of MemberUser
         * @param BizPoltalSessionContext
         */

        //public static void SendEmail(IList<MemberUser> memberUsers, BizPortalSessionContext Context)
        //{
        //    //string mailServer = ConfigurationManager.AppSettings["MailServer"];
        //    //string mailServerPassword = ConfigurationManager.AppSettings["MailServerPassword"];
        //    string senderMail = ConfigurationManager.AppSettings["Email_Sender"];
        //    string userPassword = "";
        //    StringBuilder sberror = new StringBuilder();

        //    // mail Server
        //    //System.Net.NetworkCredential cred = new System.Net.NetworkCredential(mailServer, mailServerPassword);
        //    // send form
        //    System.Net.Mail.SmtpClient SmtpMail = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["Email_SmtpAddress"]);

        //    //SmtpMail.UseDefaultCredentials = Boolean.Parse(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        //    //SmtpMail.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["MailEnableSsl"]);
        //    //SmtpMail.Credentials = cred;
        //    SmtpMail.Port = Int32.Parse(ConfigurationManager.AppSettings["Email_Port"].ToString());

        //    foreach (MemberUser memberUser in memberUsers)
        //    {
        //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //        mail.From = new System.Net.Mail.MailAddress(senderMail);
        //        userPassword = SelfAuthenticatedUser.GeneratePassword(BizPortalConfiguration.CurrentConfiguration.Security);
        //        SelfAuthenticatedUser u = ((SelfAuthenticatedUser)memberUser.User);
        //        u.ResetPassword(userPassword);
        //        u.MustChangePasswordAtNextLogon = true;
        //        u.Persist(Context);

        //        // send to customer
        //        mail.To.Add(memberUser.User.EMailAddress);
        //        mail.Subject = "Welcome To BizPoltal : " + memberUser.User.Name.NameWithoutAffixes;
        //        mail.IsBodyHtml = true;
        //        mail.Body = bodyMail(memberUser.User.LoginName, userPassword);

        //        try
        //        {
        //            SmtpMail.Send(mail);
        //            SMSService.SendSms(u.MobilePhoneNumber, CIMB.Adapter.CIMBSMS.SmsLanguageType.EN, "Password : " + userPassword);//By kittikun
        //            mail = null;
        //        }
        //        catch (SmtpFailedRecipientsException ex)
        //        {
        //            for (int i = 0; i < ex.InnerExceptions.Length; i++)
        //            {
        //                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
        //                if (status == SmtpStatusCode.MailboxBusy ||
        //                  status == SmtpStatusCode.MailboxUnavailable)
        //                {
        //                    sberror.Append("Mailbox busy or unavailable");
        //                }
        //                else
        //                {
        //                    sberror.Append("Failed to deliver message to "
        //                      + ex.InnerExceptions[i].FailedRecipient);
        //                }
        //            }
        //        }
        //        catch (SmtpException ex)
        //        {
        //            sberror.Append(ex.Message);
        //        }
        //    }
        //}

        /*
        * Method Send Email
        * @param MemberUser
        * @param BizPoltalSessionContext
        */

        //public static bool SendEmail(MemberUser memberUser, BizPortalSessionContext Context)
        //{
        //    //string mailServer = ConfigurationManager.AppSettings["MailServer"];
        //    //string mailServerPassword = ConfigurationManager.AppSettings["MailServerPassword"];
        //    string senderMail = ConfigurationManager.AppSettings["Email_Sender"];
        //    string userPassword = "";
        //    StringBuilder sberror = new StringBuilder();
        //    bool sucess = true;

        //    // mail Server
        //    //System.Net.NetworkCredential cred = new System.Net.NetworkCredential(mailServer, mailServerPassword);
        //    // send form
        //    System.Net.Mail.SmtpClient SmtpMail = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["Email_SmtpAddress"]);

        //    //SmtpMail.UseDefaultCredentials = Boolean.Parse(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        //    //SmtpMail.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["MailEnableSsl"]);
        //    //SmtpMail.Credentials = cred;
        //    SmtpMail.Port = Int32.Parse(ConfigurationManager.AppSettings["Email_Port"].ToString());

        //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //    mail.From = new System.Net.Mail.MailAddress(senderMail);

        //    if (memberUser.User is SelfAuthenticatedUser)
        //    {
        //        userPassword = SelfAuthenticatedUser.GeneratePassword(BizPortalConfiguration.CurrentConfiguration.Security);
        //        SelfAuthenticatedUser selfAuthenticatedUser = ((SelfAuthenticatedUser)memberUser.User);
        //        selfAuthenticatedUser.ResetPassword(userPassword);
        //        selfAuthenticatedUser.MustChangePasswordAtNextLogon = true;
        //        selfAuthenticatedUser.Persist(Context);

        //        // send to customer
        //        mail.To.Add(memberUser.User.EMailAddress);
        //        mail.Subject = "Welcome To BizPoltal : " + memberUser.User.Name.NameWithoutAffixes;

        //        mail.IsBodyHtml = true;
        //        mail.Body = bodyMail(memberUser.User.LoginName, userPassword);
        //        try
        //        {
        //            SmtpMail.Send(mail);
        //            SMSService.SendSms(selfAuthenticatedUser.MobilePhoneNumber, CIMB.Adapter.CIMBSMS.SmsLanguageType.EN, "Password : " + userPassword);//By kittikun
        //            mail = null;
        //        }
        //        catch (SmtpFailedRecipientsException ex)
        //        {
        //            sucess = false;
        //            for (int i = 0; i < ex.InnerExceptions.Length; i++)
        //            {
        //                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
        //                if (status == SmtpStatusCode.MailboxBusy ||
        //                  status == SmtpStatusCode.MailboxUnavailable)
        //                {
        //                    sberror.Append("Mailbox busy or unavailable");
        //                }
        //                else
        //                {
        //                    sberror.Append("Failed to deliver message to "
        //                      + ex.InnerExceptions[i].FailedRecipient);
        //                }
        //            }
        //        }
        //        catch (SmtpException ex)
        //        {
        //            sucess = false;
        //            sberror.Append(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        ActiveDirectoryUser activeDirectoryUser = ((ActiveDirectoryUser)memberUser.User);
        //        activeDirectoryUser.Persist(Context);
        //    }
        //    return sucess;
        //}

        //public static bool IsabayaSendEmail(MemberUser memberUser, BizPortalSessionContext Context)
        //{
        //    string mailServer = ConfigurationManager.AppSettings["MailServer"];
        //    string mailServerPassword = ConfigurationManager.AppSettings["MailServerPassword"];

        //    //string senderMail = ConfigurationManager.AppSettings["Email_Sender"];
        //    string userPassword = "";
        //    StringBuilder sberror = new StringBuilder();
        //    bool sucess = true;

        //    // mail Server
        //    System.Net.NetworkCredential cred = new System.Net.NetworkCredential(mailServer, mailServerPassword);

        //    // send form
        //    System.Net.Mail.SmtpClient SmtpMail = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpClient"]);
        //    SmtpMail.UseDefaultCredentials = Boolean.Parse(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        //    SmtpMail.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["MailEnableSsl"]);
        //    SmtpMail.Credentials = cred;
        //    SmtpMail.Port = Int32.Parse(ConfigurationManager.AppSettings["MailPort"].ToString());

        //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //    mail.From = new System.Net.Mail.MailAddress(mailServer);

        //    if (memberUser.User is SelfAuthenticatedUser)
        //    {
        //        //userPassword = SelfAuthenticatedUser.GeneratePassword(BizPortalConfiguration.CurrentConfiguration.Security);
        //        SelfAuthenticatedUser selfAuthenticatedUser = ((SelfAuthenticatedUser)memberUser.User);
        //        selfAuthenticatedUser.ResetPassword("aA=11111");
        //        selfAuthenticatedUser.MustChangePasswordAtNextLogon = true;
        //        selfAuthenticatedUser.Persist(Context);

        //        // send to customer
        //        mail.To.Add(memberUser.User.EMailAddress);
        //        mail.Subject = "Welcome To BizPoltal : " + memberUser.User.Name.NameWithoutAffixes;

        //        mail.IsBodyHtml = true;
        //        mail.Body = bodyMail(memberUser.User.LoginName, userPassword);
        //        try
        //        {
        //            SmtpMail.Send(mail);

        //            //SMSService.SendSms(selfAuthenticatedUser.MobilePhoneNumber, CIMB.Adapter.CIMBSMS.SmsLanguageType.EN, "Password : " + userPassword);//By kittikun
        //            mail = null;
        //        }
        //        catch (SmtpFailedRecipientsException ex)
        //        {
        //            sucess = false;
        //            for (int i = 0; i < ex.InnerExceptions.Length; i++)
        //            {
        //                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
        //                if (status == SmtpStatusCode.MailboxBusy ||
        //                  status == SmtpStatusCode.MailboxUnavailable)
        //                {
        //                    sberror.Append("Mailbox busy or unavailable");
        //                }
        //                else
        //                {
        //                    sberror.Append("Failed to deliver message to "
        //                      + ex.InnerExceptions[i].FailedRecipient);
        //                }
        //            }
        //        }
        //        catch (SmtpException ex)
        //        {
        //            sucess = false;
        //            sberror.Append(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        ActiveDirectoryUser activeDirectoryUser = ((ActiveDirectoryUser)memberUser.User);
        //        activeDirectoryUser.Persist(Context);
        //    }
        //    return sucess;
        //}

        //public static bool IsabayaSendEmailMGATETest(MemberUser memberUser, BizPortalSessionContext Context)
        //{
        //    string mailServer = ConfigurationManager.AppSettings["MailServer"];
        //    string mailServerPassword = ConfigurationManager.AppSettings["MailServerPassword"];

        //    //string senderMail = ConfigurationManager.AppSettings["Email_Sender"];
        //    string userPassword = "";
        //    StringBuilder sberror = new StringBuilder();
        //    bool sucess = true;

        //    // mail Server
        //    System.Net.NetworkCredential cred = new System.Net.NetworkCredential(mailServer, mailServerPassword);

        //    // send form
        //    SmtpClient SmtpMail = new SmtpClient(ConfigurationManager.AppSettings["SmtpClient"]);
        //    SmtpMail.UseDefaultCredentials = Boolean.Parse(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        //    SmtpMail.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["MailEnableSsl"]);
        //    SmtpMail.Credentials = cred;
        //    SmtpMail.Port = Int32.Parse(ConfigurationManager.AppSettings["MailPort"].ToString());

        //    MailMessage mail = new MailMessage();
        //    mail.From = new MailAddress(mailServer);

        //    if (memberUser.User is SelfAuthenticatedUser)
        //    {
        //        userPassword = BizPortalConfiguration.CurrentConfiguration.Security.GeneratePassword();
        //        SelfAuthenticatedUser selfAuthenticatedUser = ((SelfAuthenticatedUser)memberUser.User);
        //        selfAuthenticatedUser.ResetPassword(userPassword);
        //        selfAuthenticatedUser.MustChangePasswordAtNextLogon = true;
        //        selfAuthenticatedUser.Persist(Context);

        //        // send to customer
        //        mail.To.Add(memberUser.User.EMailAddress);
        //        mail.Subject = "Welcome To MGATE : " + memberUser.User.Name.NameWithoutAffixes;

        //        mail.IsBodyHtml = true;
        //        mail.Body = bodyMailP(memberUser.User.LoginName, userPassword);
        //        try
        //        {
        //            SmtpMail.Send(mail);

        //            //SMSService.SendSms(selfAuthenticatedUser.MobilePhoneNumber, CIMB.Adapter.CIMBSMS.SmsLanguageType.EN, "Password : " + userPassword);//By kittikun
        //            mail = null;
        //        }
        //        catch (SmtpFailedRecipientsException ex)
        //        {
        //            sucess = false;
        //            for (int i = 0; i < ex.InnerExceptions.Length; i++)
        //            {
        //                SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
        //                if (status == SmtpStatusCode.MailboxBusy ||
        //                  status == SmtpStatusCode.MailboxUnavailable)
        //                {
        //                    sberror.Append("Mailbox busy or unavailable");
        //                }
        //                else
        //                {
        //                    sberror.Append("Failed to deliver message to "
        //                      + ex.InnerExceptions[i].FailedRecipient);
        //                }
        //            }
        //        }
        //        catch (SmtpException ex)
        //        {
        //            sucess = false;
        //            sberror.Append(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        ActiveDirectoryUser activeDirectoryUser = ((ActiveDirectoryUser)memberUser.User);
        //        activeDirectoryUser.Persist(Context);
        //    }
        //    return sucess;
        //}

        //By 1ShLerm

        //public static bool TestSendEmail(MemberUser memberUser, string body)
        //{
        //    string mailServer = ConfigurationManager.AppSettings["MailServer"];
        //    string mailServerPassword = ConfigurationManager.AppSettings["MailServerPassword"];

        //    StringBuilder sberror = new StringBuilder();
        //    bool sucess = true;

        //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

        //    //mail.From = new System.Net.Mail.MailAddress("admin@isabaya.net");

        //    // mail Server
        //    System.Net.NetworkCredential cred = new System.Net.NetworkCredential(mailServer, mailServerPassword);

        //    // send form
        //    System.Net.Mail.SmtpClient SmtpMail = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["SmtpClient"]);
        //    SmtpMail.UseDefaultCredentials = Boolean.Parse(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        //    SmtpMail.EnableSsl = Boolean.Parse(ConfigurationManager.AppSettings["MailEnableSsl"]);
        //    SmtpMail.Credentials = cred;
        //    SmtpMail.Port = Int32.Parse(ConfigurationManager.AppSettings["MailPort"].ToString());

        //    // send to ....
        //    //mail.To.Add("amfeelgood@gmail.com");
        //    mail.Subject = "Test Send Email";

        //    mail.IsBodyHtml = true;
        //    mail.Body = memberUser.User.Name.NameWithoutAffixes + "<br />" + memberUser.User.EMailAddress + "<br />" + body;

        //    try
        //    {
        //        SmtpMail.Send(mail);
        //        mail = null;
        //    }
        //    catch (SmtpFailedRecipientsException ex)
        //    {
        //        sucess = false;
        //        for (int i = 0; i < ex.InnerExceptions.Length; i++)
        //        {
        //            SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
        //            if (status == SmtpStatusCode.MailboxBusy ||
        //              status == SmtpStatusCode.MailboxUnavailable)
        //            {
        //                sberror.Append("Mailbox busy or unavailable");
        //            }
        //            else
        //            {
        //                sberror.Append("Failed to deliver message to "
        //                  + ex.InnerExceptions[i].FailedRecipient);
        //            }
        //        }
        //    }
        //    catch (SmtpException ex)
        //    {
        //        sucess = false;
        //        sberror.Append(ex.Message);
        //    }

        //    return sucess;
        //}

        public static bool SendEmailContactTheBank(MemberUser memberUser, string body)
        {
            string senderMail = ConfigurationManager.AppSettings["Email_Sender"];
            StringBuilder sberror = new StringBuilder();
            bool sucess = true;

            System.Net.Mail.SmtpClient SmtpMail = new System.Net.Mail.SmtpClient(ConfigurationManager.AppSettings["Email_SmtpAddress"]);
            SmtpMail.Port = Int32.Parse(ConfigurationManager.AppSettings["Email_Port"].ToString());

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress(senderMail);

            // send to customer
            mail.To.Add(ConfigurationManager.AppSettings["EmailContactTheBank"]);//Sent to CIMB
            mail.Subject = "iBank E - จากคุณ " + memberUser.Name.NameWithoutAffixes;

            mail.IsBodyHtml = true;
            mail.Body = "อีเมลจากคุุณ " + memberUser.Name.NameWithoutAffixes + " : " + memberUser.EMailAddress + "<br /><br />" + body;
            try
            {
                SmtpMail.Send(mail);
                mail = null;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                sucess = false;
                for (int i = 0; i < ex.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                      status == SmtpStatusCode.MailboxUnavailable)
                    {
                        sberror.Append("Mailbox busy or unavailable");
                    }
                    else
                    {
                        sberror.Append("Failed to deliver message to "
                          + ex.InnerExceptions[i].FailedRecipient);
                    }
                }
            }
            catch (SmtpException ex)
            {
                sucess = false;
                sberror.Append(ex.Message);
            }

            return sucess;
        }

        /*
        * Method Genarate Mail body
        * @param string of username
        * @param string of password
        * @return string of body mail
        */

        private static string bodyMail(string username, string pass)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("UserName : {0}", username));//By kittikun

            //sb.Append(String.Format("Password : {0}", pass));
            return sb.ToString();
        }

        private static string bodyMailP(string username, string pass)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("<b>UserName</b> : {0}<br/>", username));//By kittikun

            sb.Append(String.Format("<b>Password</b> : {0}", pass));
            return sb.ToString();
        }

        //help Approve
        public static void Remark(params string[] str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                sb.Append(string.Format("<b>ผู้ให้บริการ</b> : {i} <br/>", str[i]));
                sb.Append(string.Format("<b>บัญชี</b> : {i} <br/>", str[i]));
                sb.Append(string.Format("<b>จำนวนเงิน</b> : {i} <br/>", str[i]));
            }
        }

        /// <summary>
        /// Function for get name for BizPortalFunction
        /// </summary>
        /// <param name="FunctionID">Function ID</param>
        /// <returns>Name Function</returns>
        //public static string GetFunctionNameByID(int FunctionID)
        //{
        //    foreach (BizPortalFunction item in BankAdminFunctions.Functions)
        //    {
        //        if (item.ID == FunctionID)
        //            return item.Title.ToString();
        //    }

        //    foreach (BizPortalFunction item in BankMaker.Functions)
        //    {
        //        if (item.ID == FunctionID)
        //            return item.Title.ToString();
        //    }

        //    foreach (BizPortalFunction item in ClientAdmin.Functions)
        //    {
        //        if (item.ID == FunctionID)
        //            return item.Title.ToString();
        //    }

        //    foreach (BizPortalFunction item in ClientMaker.Functions)
        //    {
        //        if (item.ID == FunctionID)
        //            return item.Title.ToString();
        //    }

        //    return "";
        //}
    }

    public class News
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Detail { get; set; }

        public string Link { get; set; }

        public string Maker { get; set; }

        public string MakerDateTime { get; set; }

        public News()
        {
        }
    }
}