using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MyBudget.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace MyBudget.Utility
{
    public class GmailSettings
    {
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static ApplicationDbContext db = new ApplicationDbContext();

        public static List<Message> GetMail()
        {
            UserCredential credential;
            string startupPath = AppDomain.CurrentDomain.BaseDirectory;
            using (var stream = new System.IO.FileStream(startupPath + @"credentials.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = startupPath + @"token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }


            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "My Budget",
            });

            ListLabelsResponse response = service.Users.Labels.List("me").Execute();
            List<Label> _labels = new List<Label>();
            foreach (Label label in response.Labels)
            {
                Label _label = new Label();
                //  if(label.Type == "user")
                // {
                _label.Id = label.Id;
                _label.Name = label.Name;
                //}
                _labels.Add(_label);
            }
            String query = "";
            List<Message> result = new List<Message>();
            // Define parameters of request.
            UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List("me");
            request.Q = query;
            request.MaxResults = 5000;
            request.LabelIds = "Label_2500152685495181559";
            // var serv = service.Users.Messages
            try
            {
                ListMessagesResponse response2 = request.Execute();
                result.AddRange(response2.Messages);
                //request.PageToken = response2.NextPageToken;

                //foreach (var item in result)
                for (int i = 0; i < result.Count; i++)
                {
                    var m = service.Users.Messages.Get("me", result[i].Id).Execute();
                    //if (m.LabelIds.Contains("IMPORTANT") && m.LabelIds.Contains("Label_2500152685495181559"))
                    //{
                    var d = m.Snippet;
                    var model = new SMSData();
                    string[] msg = d.Split(new string[] { "Message text: " }, StringSplitOptions.None);
                    model.SMSText = d;// msg[1];
                    string sa = @"""" + "/Date(" + m.InternalDate + "-0530 )/" + @"""";
                    model.Date = DateTime.Now;// JsonConvert.DeserializeObject<DateTime>(sa);
                    //model.Label = _labels.Where(x => x.Id == m.LabelIds)
                    if (db.SMSData.Count() > 0)
                    {
                        DateTime prevDate = db.SMSData.ToList().OrderByDescending(x => x.Date.Value).Select(x => x.Date.Value).First();
                        if (model.Date > prevDate)
                        {
                            if (d.Contains("KOTAK") || d.Contains("XXXX467304"))
                            {
                                model.BankName = "Kotak Mahindra Bank";

                                if (d.Contains("credited "))
                                {
                                    model.TransactionType = "Credit";
                                    // resultString = Regex.Match(subjectString, @"\d+").Value;
                                    try
                                    {
                                        string[] msg1 = msg[1].Split(new string[] { "credited Rs." }, StringSplitOptions.None);
                                        string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                        BankAccounts ba = new BankAccounts();
                                        ba = db.BankAccounts.Where(x => x.BankName.Contains("KOTAK")).FirstOrDefault();
                                        ba.Balance += Convert.ToDouble(msg2[0]);
                                        model.Amount = msg2[0];
                                        db.Entry(ba).State = EntityState.Modified;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("An error occurred: " + ex.Message);
                                    }

                                }
                                else if (d.Contains("debited"))
                                {
                                    try
                                    {

                                        model.TransactionType = "Debit";
                                        string[] msg1 = msg[1].Split(new string[] { "has been debited" }, StringSplitOptions.None);
                                        // string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                        BankAccounts ba = new BankAccounts();
                                        ba = db.BankAccounts.Where(x => x.BankName.Contains("KOTAK")).FirstOrDefault();
                                        ba.Balance -= Convert.ToDouble(msg1[0]);
                                        model.Amount = msg1[0];
                                        db.Entry(ba).State = EntityState.Modified;

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("An error occurred: " + ex.Message);
                                    }
                                }

                                db.SaveChanges();
                            }
                            else if (d.Contains("ICICI") || d.Contains("XX3416") || d.Contains("XX416"))
                            {
                                model.BankName = "ICICI Bank";

                                if (d.Contains("credited "))
                                {
                                    try
                                    {
                                        model.TransactionType = "Credit";

                                        string[] msg1 = msg[1].Split(new string[] { "credited Rs." }, StringSplitOptions.None);
                                        string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                        BankAccounts ba = new BankAccounts();
                                        ba = db.BankAccounts.Where(x => x.BankName.Contains("ICICI") && x.AccountType == 0).FirstOrDefault();
                                        ba.Balance += Convert.ToDouble(msg2[0]);
                                        model.Amount = msg2[0];
                                        db.Entry(ba).State = EntityState.Modified;

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("An error occurred: " + ex.Message);
                                    }
                                }
                                else if (d.Contains("debited"))
                                {

                                    model.TransactionType = "Debit";
                                    if (msg[1].Contains("INR"))
                                    {
                                        try
                                        {

                                            string[] msg1 = msg[1].Split(new string[] { "INR" }, StringSplitOptions.None);
                                            string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                            BankAccounts ba = new BankAccounts();
                                            ba = db.BankAccounts.Where(x => x.BankName.Contains("ICICI") && x.AccountType == 0).FirstOrDefault();
                                            ba.Balance -= Convert.ToDouble(msg2[0]);
                                            model.Amount = msg2[0];
                                            db.Entry(ba).State = EntityState.Modified;

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("An error occurred: " + ex.Message);
                                        }
                                    }
                                    else if (msg[1].Contains("has been debited"))
                                    {
                                        try
                                        {
                                            string[] msg1 = msg[1].Split(new string[] { "has been debited for Rs." }, StringSplitOptions.None);
                                            string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                            BankAccounts ba = new BankAccounts();
                                            ba = db.BankAccounts.Where(x => x.BankName.Contains("ICICI") && x.AccountType == 0).FirstOrDefault();
                                            ba.Balance -= Convert.ToDouble(msg2[0]);
                                            model.Amount = msg2[0];
                                            db.Entry(ba).State = EntityState.Modified;

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("An error occurred: " + ex.Message);
                                        }
                                    }

                                }

                                db.SaveChanges();
                            }
                            else if (d.Contains("Union") || d.Contains("X4541") || d.Contains("**24541"))
                            {
                                model.BankName = "Union Bank";

                                if (d.Contains("credited "))
                                {
                                    try
                                    {
                                        model.TransactionType = "Credit";
                                        // resultString = Regex.Match(subjectString, @"\d+").Value;
                                        string[] msg1 = msg[1].Split(new string[] { "credited Rs." }, StringSplitOptions.None);
                                        string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                        BankAccounts ba = new BankAccounts();
                                        ba = db.BankAccounts.Where(x => x.BankName.Contains("Union") && x.AccountType == 0).FirstOrDefault();
                                        ba.Balance += Convert.ToDouble(msg2[0]);
                                        model.Amount = msg2[0];
                                        db.Entry(ba).State = EntityState.Modified;

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("An error occurred: " + ex.Message);
                                    }
                                }
                                else if (d.Contains("debited"))
                                {
                                    try
                                    {
                                        model.TransactionType = "Debit";
                                        string[] msg1 = msg[1].Split(new string[] { "is debited for Rs." }, StringSplitOptions.None);
                                        string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                        BankAccounts ba = new BankAccounts();
                                        ba = db.BankAccounts.Where(x => x.BankName.Contains("Union") && x.AccountType == 0).FirstOrDefault();
                                        ba.Balance -= Convert.ToDouble(msg2[0]);
                                        model.Amount = msg2[0];
                                        db.Entry(ba).State = EntityState.Modified;

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("An error occurred: " + ex.Message);
                                    }
                                }

                                db.SaveChanges();
                            }
                            else if (d.Contains("SB A/c") || d.Contains("SBI UPI"))
                            {
                                model.BankName = "SBI";

                                if (d.Contains("credited "))
                                {
                                    try
                                    {
                                        model.TransactionType = "Credit";
                                        // resultString = Regex.Match(subjectString, @"\d+").Value;
                                        string[] msg1 = msg[1].Split(new string[] { "credited Rs." }, StringSplitOptions.None);
                                        string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                        BankAccounts ba = new BankAccounts();
                                        ba = db.BankAccounts.Where(x => x.BankName.Contains("SBI-H") && x.AccountType == 0).FirstOrDefault();
                                        ba.Balance += Convert.ToDouble(msg2[0]);
                                        model.Amount = msg2[0];
                                        db.Entry(ba).State = EntityState.Modified;

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("An error occurred: " + ex.Message);
                                    }
                                }
                                else if (d.Contains("debited"))
                                {
                                    if (msg[1].Contains("INR"))
                                    {
                                        try
                                        {

                                            string[] msg1 = msg[1].Split(new string[] { "INR" }, StringSplitOptions.None);
                                            string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                            BankAccounts ba = new BankAccounts();
                                            ba = db.BankAccounts.Where(x => x.BankName.Contains("SBI-H") && x.AccountType == 0).FirstOrDefault();
                                            ba.Balance -= Convert.ToDouble(msg2[0]);
                                            model.Amount = msg2[0];
                                            db.Entry(ba).State = EntityState.Modified;

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("An error occurred: " + ex.Message);
                                        }
                                    }
                                    else if (msg[1].Contains("is debited for"))
                                    {
                                        try
                                        {
                                            model.TransactionType = "Debit";
                                            string[] msg1 = msg[1].Split(new string[] { "is debited for Rs." }, StringSplitOptions.None);
                                            string[] msg2 = msg1[1].Split(new string[] { "on" }, StringSplitOptions.None);
                                            BankAccounts ba = new BankAccounts();
                                            ba = db.BankAccounts.Where(x => x.BankName.Contains("SBI-H") && x.AccountType == 0).FirstOrDefault();
                                            ba.Balance -= Convert.ToDouble(msg2[0]);
                                            model.Amount = msg2[0];
                                            db.Entry(ba).State = EntityState.Modified;

                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("An error occurred: " + ex.Message);
                                        }
                                    }

                                }

                                db.SaveChanges();
                            }


                            //if (db.SMSData.Count() > 0)
                            //{
                            //    DateTime prevDate = db.SMSData.ToList().OrderByDescending(x => x.Date.Value).Select(x => x.Date.Value).First();
                            //    if (model.Date > prevDate)
                            //    {
                            db.SMSData.Add(model);
                        }
                    }
                    else
                    {
                        db.SMSData.Add(model);
                    }
                    // Console.WriteLine(i);
                }

                // }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            return result;
        }


public static List<Message> ListMessages(GmailService service, String userId, String query)
{
    List<Message> result = new List<Message>();
    UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(userId);
    request.Q = query;

    do
    {
        try
        {
            ListMessagesResponse response = request.Execute();
            result.AddRange(response.Messages);
            request.PageToken = response.NextPageToken;
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
    } while (!String.IsNullOrEmpty(request.PageToken));

    return result;
}

    }
}