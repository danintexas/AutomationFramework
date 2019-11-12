using AutomationFramework.Tests;
using Newtonsoft.Json;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

class AdditionalFunctions
{
    /// <summary>
    /// This method is the heavy lifter to get all email messages from a POP3 account. This is called by the GetAllEmailsFromAnEmailAccount method
    /// in the Core.cs
    /// </summary>
    /// <param name="hostname">Domain</param>
    /// <param name="port">Usually always 110 for Pop3</param>
    /// <param name="useSsl">SSL true or false</param>
    /// <param name="username">Email Account</param>
    /// <param name="password">Password for the account</param>
    /// <param name="delete">Delete all emails from the account after the account's emails are saved.</param>
    /// <returns></returns>
    public static List<Message> FetchAllMessages(string hostname, int port, bool useSsl, string username, string password, bool delete = false)
    {
        using (Pop3Client client = new Pop3Client())
        {
            client.Connect(hostname, port, useSsl);
            client.Authenticate(username, password);
            int messageCount = client.GetMessageCount();

            List<Message> allMessages = new List<Message>(messageCount);

            // Messages are numbered in the interval: [1, messageCount]
            // Ergo: message numbers are 1-based.
            // Most servers give the latest message the highest number
            for (int i = messageCount; i > 0; i--)
            {
                allMessages.Add(client.GetMessage(i));
            }

            int number = 1;

            // Now return the fetched messages
            foreach (var emailText in allMessages)
            {
                DateTime date = DateTime.Today;
                var dir = $@"c:\Automation Logs\{date:MM.dd.yyyy}\Emails\";

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                MessagePart plainText = emailText.FindFirstPlainTextVersion();
                plainText.Save(new FileInfo(dir + $"\\Email {number}.txt"));
                number++;
            }

            // Delete all messages from the account
            if (delete == true)
            {
                try
                {
                    client.DeleteAllMessages();
                }
                catch (Exception _ex)
                {
                    Console.WriteLine("Error occured trying to delete with FetchAllMessages method." + Environment.NewLine + _ex);
                }
            }

            return allMessages;
        }
    }

    /// <summary>
    /// QueryDatabase method is meant to be called by DatabaseCheck method in the Core
    /// </summary>
    /// <param name="query">Query to run against the database</param>
    /// <param name="expected">Expected results from the query</param>
    /// <param name="connectionString">Database connection information</param>
    /// <returns></returns>
    public static string QueryDatabase(string query, string connectionString)
    {
        string results = "";

        using (SqlConnection connection =
            new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        results = reader.GetValue(0).ToString();
                    }
                }
                else
                {
                    Console.WriteLine("No result found.");
                }
                
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                results = "Something happened with the SQL connection. Check credentials. Exception: " + Environment.NewLine + ex;
            }
        }

        return results; 
    }

    /// <summary>
    /// PhotoSelection will select an image to upload from the 'Support\Image Bank' folder of the framework. 
    /// Known bug currently with .Net 2.2 - will be resolved with Core 3.0
    /// </summary>
    /// <param name="selection">Select the number of the automation image #.png to use. You can also enter '0' for this field to randomly pick an image</param>
    /// <param name="fileName">Optional filename to upload. If using this the selection field will be ignored.</param>
    public static void PhotoSelection(int selection = 0, string fileName = null)
    {
        string homeDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Core)).Location);
        string trueFilename;
        
        if (fileName == null)
        {
            int fCount = Directory.GetFiles($"{homeDirectory}\\Support\\Image Bank\\", "automation image*", SearchOption.TopDirectoryOnly).Length;

            if (selection == 0)
            {
                Random random = new Random();
                selection = random.Next(1, (fCount + 1));
            }

            else if (selection > fCount)
            {
                Random random = new Random();
                selection = random.Next(1, (fCount + 1));
            }

            trueFilename = $"automation image {selection}.png";
        }

        else
        {
            trueFilename = fileName;
        }

        // Below generates an error because Core 2.2 does not support SendKeys. 
        // 3.0 should support this. Once this is migrated to 3.0 then all the .Net 4.6 assemblies can be removed from the project's Dependencies
        System.Windows.Forms.SendKeys.SendWait($"{homeDirectory}\\Support\\Image Bank\\" + trueFilename);
        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
    }

    public static async Task IntegrateWithSlackAsync(string message = "")
    {
        var webhookUrl = new Uri("https://hooks.slack.com/services/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"); // This is unique for RumbleOn clsautomation-status channel
        var slackClient = new SlackClient(webhookUrl);
        var response = await slackClient.SendMessageAsync(message);
        var isValid = response.IsSuccessStatusCode ? "valid" : "invalid";
        Console.WriteLine($"Received {isValid} response.");
    }
    public class SlackClient
    {
        private readonly Uri _webhookUrl;
        private readonly HttpClient _httpClient = new HttpClient();

        public SlackClient(Uri webhookUrl)
        {
            _webhookUrl = webhookUrl;
        }

        public async Task<HttpResponseMessage> SendMessageAsync(string message, string channel = null, string username = null)
        {
            var payload = new
            {
                text = message,
                channel,
                username,
            };
            var serializedPayload = JsonConvert.SerializeObject(payload);
            var response = await _httpClient.PostAsync(_webhookUrl,
                new StringContent(serializedPayload, Encoding.UTF8, "application/json"));

            return response;
        }
    }
}
