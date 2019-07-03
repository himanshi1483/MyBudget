using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MyBudget.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            // List labels.
            // var serv = service.Users.Messages
            try
            {
                ListMessagesResponse response2 = request.Execute();
                result.AddRange(response2.Messages);
                request.PageToken = response2.NextPageToken;
                request.MaxResults =500;
                foreach (var item in result)
                {
                    var m = service.Users.Messages.Get("me", item.Id).Execute();
                    //if (m.LabelIds.Contains("IMPORTANT") && m.LabelIds.Contains("Label_2500152685495181559"))
                   // {
                        var d = m.Snippet;
                        var model = new SMSData();
                        string[] msg = d.Split(new string[] { "Message text: " }, StringSplitOptions.None);
                        model.SMSText = d;// msg[1];
                                                         
                        string sa = @"""" + "/Date(" + m.InternalDate + "-0530 )/" + @"""";
                         model.Date = DateTime.Now;//JsonConvert.DeserializeObject<DateTime>(sa);
                        //model.Label = _labels.Where(x=>x.Id == m.LabelIds)
                        //DateTime prevDate = db.SMSData.ToList().OrderByDescending(x => x.Date).Select(x => x.Date).First();
                      //  if(model.Date > prevDate)
                         db.SMSData.Add(model);
                  //  }
                  
                }
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