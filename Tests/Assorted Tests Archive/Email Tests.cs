using NUnit.Framework;
using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutomationFramework.Tests.Development_Tests
{

    [Category("Dev Tests")]
    partial class SmokeTest : Core
    {
        public int number = 1;

        [TestCase(TestName = "Check Email")]
        [Order(99)]
        
        public void GetMessages()
        {
            FetchAllMessages("danintexas.com", 110, false, "dan.test.5.16.4@danintexas.com", "Rumbleon12");
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

                // Now return the fetched messages
                allMessages.ForEach(FindPlainTextInMessage);
                Console.WriteLine(allMessages);
                return allMessages;
            }
        }
        public static void FindPlainTextInMessage(Message message)
        {
            MessagePart plainText = message.FindFirstPlainTextVersion();
            if (plainText != null)
            {
                int number = 1;
                // Save the plain text to a file, database or anything you like
                do
                {
                    plainText.Save(new FileInfo($"c:\\Automation Logs\\plainText{number}.txt"));
                    
                }
                while (!File.Exists($"c:\\Automation Logs\\plainText{number}.txt"));
                number++;
            }
        }
    }
}
