using AutomationFramework.Tests;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

class AdditionalFunctions
{
    /// <summary>
    /// PhotoSelection will select an image to upload from the 'Support\Image Bank' folder of the framework.
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

            /* The above generates the following error with a 3 X .Net dialog
             * $exception	{"Could not load type 'System.Runtime.InteropServices.StandardOleMarshalObject' from assembly 
             * 'System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.":"System.Runtime.InteropServices.StandardOleMarshalObject"}	System.TypeLoadException
             */
        }

        else
        {
            trueFilename = fileName;
        }
        
        System.Windows.Forms.SendKeys.SendWait($"{homeDirectory}\\Support\\Image Bank\\" + trueFilename);
        System.Windows.Forms.SendKeys.SendWait("{ENTER}");
    }

    public static List<Message> FetchAllMessages(string hostname, int port, bool useSsl, string username, string password)
    {
        // The client disconnects from the server when being disposed
        using (Pop3Client client = new Pop3Client())
        {
            // Connect to the server
            client.Connect(hostname, port, useSsl);

            // Authenticate ourselves towards the server
            client.Authenticate(username, password);

            // Get the number of messages in the inbox
            int messageCount = client.GetMessageCount();

            // We want to download all messages
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
                MessagePart plainText = emailText.FindFirstPlainTextVersion();
                var body = plainText.GetBodyAsText();
                Console.WriteLine(body);
                plainText.Save(new FileInfo($"c:\\Automation Logs\\Email {number}.txt"));
                number++;
            }

            // Delete all messages from the account
            client.DeleteAllMessages();

            return allMessages;
        }
    }
}
