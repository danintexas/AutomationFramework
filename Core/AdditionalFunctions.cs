using AutomationFramework.Tests;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

class AdditionalFunctions
{
    /// <summary>
    /// GenerateRandomVINFromTemplate is a method made specifically for RumbleOn. This will take a truncated vehicle VIN 
    /// missing the last three digits - apply a random 3 digits to the end and then change the VIN
    /// check digit to its correct value.
    /// </summary>
    /// <param name="vin">Truncated auto VIN missing the last three digits</param>
    /// <returns></returns>
    public static string GenerateRandomVINFromTemplate(string vin)
    {
        var result = 0;
        var index = 0;
        var checkDigit = 0;
        var checkSum = 0;
        var weight = 0;
        int randomNumber = (new Random()).Next(100, 1000);
        vin += randomNumber.ToString();

        foreach (var c in vin.ToCharArray())
        {
            index++;
            var character = c.ToString().ToLower();
            if (char.IsNumber(c))
                result = int.Parse(character);
            else
            {
                switch (character)
                {
                    case "a":
                    case "j":
                        result = 1;
                        break;
                    case "b":
                    case "k":
                    case "s":
                        result = 2;
                        break;
                    case "c":
                    case "l":
                    case "t":
                        result = 3;
                        break;
                    case "d":
                    case "m":
                    case "u":
                        result = 4;
                        break;
                    case "e":
                    case "n":
                    case "v":
                        result = 5;
                        break;
                    case "f":
                    case "w":
                        result = 6;
                        break;
                    case "g":
                    case "p":
                    case "x":
                        result = 7;
                        break;
                    case "h":
                    case "y":
                        result = 8;
                        break;
                    case "r":
                    case "z":
                        result = 9;
                        break;
                }
            }

            if (index >= 1 && index <= 7 || index == 9)
                weight = 9 - index;
            else if (index == 8)
                weight = 10;
            else if (index >= 10 && index <= 17)
                weight = 19 - index;
            if (index == 9)
                checkDigit = character == "x" ? 10 : result;
            checkSum += (result * weight);
        }
        checkSum %= 11;

        vin = vin.Remove(8, 1).Insert(8, checkSum.ToString());
        
        return vin;
    }

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
    public static bool QueryDatabase(string query, string expected, string connectionString)
    {
        bool returnStatus = false;
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
                    Console.WriteLine("No rows found.");
                }
                
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        if (results == expected)
        {
            returnStatus = true;
        }

        return returnStatus; 
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
}
