using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1Individual
{
    public class MessageManager
    {
        DataBaseManager dBMessageManager = new DataBaseManager(); // instantiate object to access the database

        MenuManager messageMenuManager = new MenuManager(); // test for scroll menu

        InputManager messageInputManager = new InputManager();

        public void CreateMessage(string sender, string recipient) 
        {

            Console.Clear();
            string subject;
            Console.WriteLine("\nPlease type the subject of the message: ");
            subject = Console.ReadLine();
            if (subject.Length == 0)
            {
                subject = "No subject";
            }
            subject = subject.Substring(0, Math.Min(subject.Length, 30)); // Max length of subject: 30 characters

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("======= New Message =======");
            Console.WriteLine($"From       : {sender}");
            Console.WriteLine($"To         : {recipient}");
            Console.WriteLine();
            Console.WriteLine($"Subject    : {subject}");


            Console.WriteLine("\nBody:\n");
            string messageBody;
            messageBody = Console.ReadLine();
            messageBody = messageBody.Substring(0, Math.Min(messageBody.Length, 250)); // Max length of message: 250 characters
                                                                                       //checked!
            Console.WriteLine();
            Console.WriteLine($"The message you typed is:\n{messageBody}");
            Console.WriteLine();
            

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Press ENTER to send your message to {recipient} or ESC to go back and discard the message.");
            Console.ResetColor();
            ConsoleKeyInfo keyPressed;
            do
            {

                keyPressed = Console.ReadKey(true);


            }
            while (keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Escape);
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Message was discarted!");
                Console.ResetColor();
               
                return;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                // create the message and add it to the database
                using (var db = new MyContext())
                {
                    Message newMessage = new Message()
                    {
                        Subject = subject,
                        Content = messageBody,
                        Sender = db.Users.Where(x => x.Username == sender).FirstOrDefault(),
                       
                        Recipient = db.Users.Where(x => x.Username == recipient).FirstOrDefault(),
                        DateCreated = DateTime.Now,
                        IsMessageActive = true
                    };

                    db.Messages.Add(newMessage); 
                    
                    db.SaveChanges();
                    

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine();
                    Console.WriteLine("Message sent!");
                    Console.ResetColor();
                    
                }

            }
        }

        public void ShowInbox(string username) 
        {
            List<Message> inbox = new List<Message>(); // list to hold the received messages of user

            using (var db = new MyContext())
            {
                
                inbox = db.Messages.Include("Recipient").Include("Sender")
                    .Where(x => x.Recipient.Username == username && x.IsMessageActive == true).ToList();
            }

            if (inbox.Count <= 0) 
            {
                Console.Clear();
                Console.WriteLine($"\n\n\nThe Inbox of '{username}' is empty!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n\n\nPress any key to go back.");
                Console.ResetColor();
                Console.ReadKey();

                return;
            }
            
            messageMenuManager.ScrollInboxMenu(username, inbox);

        }

        public void ShowSentMessages(string username) 
        {
            List<Message> sent = new List<Message>(); 

            using (var db = new MyContext())
            {
               
                sent = db.Messages.Include("Recipient")
                    .Include("Sender").Where(x => x.Sender.Username == username && x.IsMessageActive == true).ToList();
            }

            if (sent.Count <= 0) 
            {
                Console.Clear();
                Console.WriteLine($"\n\n\n{username} hasn't sent any messages yet!");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n\n\n\nPress any key to go back.");
                Console.ResetColor();
                Console.ReadKey();

                return;
            }

            // Print the sent messages to console
            messageMenuManager.ScrollSentMenu(username, sent);

        }

        // Method of all Admins to view the messages of a user
        public void ViewUserMessages()
        {
            bool userExists;
            string usernameToViewMessages;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("======= Welcome Admin =======");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("======= Here you can view the users messages =======");
                Console.WriteLine();
                Console.WriteLine("Choose the username of the user you would like to view the messages:");
                usernameToViewMessages = messageInputManager.InputUserName(); 
                if (usernameToViewMessages is null) 
                {
                    return;
                }
                userExists = dBMessageManager.DoesUsernameExist(usernameToViewMessages); 
                if (!userExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The username you entered does not exist.\nPlease choose another user.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    string header = ($"\nWould you like to view the user's sent or received messages?\n");
                    string[] mailboxes = new string[] { "Inbox", "Sent Messages", "Go Back" };
                    do
                    {
                        int option = messageMenuManager.ScrollMenu(header, mailboxes);

                        switch (option)
                        {
                            case 0:
                                ShowInbox(usernameToViewMessages);
                                break;
                            case 1:
                                ShowSentMessages(usernameToViewMessages);
                                break;
                            case 2:
                                return;
                        }
                    } while (true);
                }
            } while (!userExists);
        }

        // Method of Super and Master Admins to delete the messages of a user
        public void DeleteMessage()
        {
            bool messageExists;
            int messageIdForDelete;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("======= Welcome Admin =======");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("======= You can delete messages! =======");
                Console.WriteLine();
                Console.WriteLine("Choose the ID of the message you would like to delete:");

                

                string input = Console.ReadLine();

                

                if (!(int.TryParse(input, out messageIdForDelete))) 
                                                                    
                {
                    Console.WriteLine("Invalid Input. Please try again.");
                    Console.ReadKey();
                    return; 
                }
                else
                {
                    messageExists = dBMessageManager.DoesMessageExist(messageIdForDelete); // check if message exists based on message ID
                    if (!messageExists)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("We are so sorry but the messageId you entered does not exist.\nPlease choose another message to delete.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        bool messageActive = dBMessageManager.IsMessageActive(messageIdForDelete); // check if message is active
                        if (!messageActive)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"The message with ID '{messageIdForDelete}' has been deleted.");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\n\nPress any key to go back.");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        else
                        {
                            using (var db = new MyContext())
                            {
                                string sender = db.Messages.Include("Sender")
                                    .Where(x => x.MessageId == messageIdForDelete).FirstOrDefault().Sender.Username;
                                string recipient = db.Messages.Include("Recipient")
                                    .Where(x => x.MessageId == messageIdForDelete).FirstOrDefault().Recipient.Username;

                                Console.WriteLine($"This is a message created from {sender} and sent to {recipient}.\nWould you like to delete it?");
                                Console.WriteLine("\nPress ESC if you want to go back or ENTER if you want to delete the message.");

                               
                                ConsoleKeyInfo keyPressed;
                                do
                                {
                                    keyPressed = Console.ReadKey(true);

                                } while (keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Escape);
                                if (keyPressed.Key == ConsoleKey.Escape)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("The message was not deleted.");
                                    Console.ResetColor();
                                    
                                    return;
                                }
                                else if (keyPressed.Key == ConsoleKey.Enter)
                                {
                                    db.Messages.Where(x => x.MessageId == messageIdForDelete).FirstOrDefault().IsMessageActive = false;
                                    db.SaveChanges();

                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Message with ID {messageIdForDelete} has been deleted.");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("\n\nPress any key to go back.");
                                    Console.ReadKey();
                                }
                            }
                        }
                    }
                }
            } while (!messageExists);
        }

        // Method of Junior, Master and Super Admins to edit the messages of a user
        public void EditMessage()
        {
            bool messageExists;
            int messageIdForEdit;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("======= Welcome Admin =======");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("======= You can edit the content of messages! =======");
                Console.WriteLine();
                Console.WriteLine("Choose the ID of the message you would like to edit:");

                
                string input = Console.ReadLine();
                

                if (!(int.TryParse(input, out messageIdForEdit))) 
                {
                    Console.WriteLine("Invalid Input. Please try again.");
                    Console.ReadKey();
                    return; 
                }
                else
                {
                    messageExists = dBMessageManager.DoesMessageExist(messageIdForEdit); // check if message exists based on message ID
                    if (!messageExists)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The messageId you entered does not exist.\nPlease choose another message to edit.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        bool messageActive = dBMessageManager.IsMessageActive(messageIdForEdit); // check if message is active
                        if (!messageActive)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"The message with ID '{messageIdForEdit}' has been deleted.");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n\nPress any key to go back.");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        else
                        {
                            using (var db = new MyContext())
                            {
                                string sender = db.Messages.Include("Sender")
                                    .Where(x => x.MessageId == messageIdForEdit).FirstOrDefault().Sender.Username;
                                string recipient = db.Messages.Include("Recipient")
                                    .Where(x => x.MessageId == messageIdForEdit).FirstOrDefault().Recipient.Username;

                                string content = db.Messages.Where(x => x.MessageId == messageIdForEdit).FirstOrDefault().Content;


                                Console.WriteLine();
                                Console.WriteLine($"This is a message created from {sender} and sent to {recipient}.");
                                Console.WriteLine($"The content of the message is:\n\n\n {content}");
                                Console.WriteLine("\n\nWould you like to edit this message?");
                                Console.WriteLine("\nPress ESC if you want to go back or ENTER if you want to edit the message.\n");

                                
                                ConsoleKeyInfo keyPressed;
                                do
                                {
                                    keyPressed = Console.ReadKey(true);

                                } while (keyPressed.Key != ConsoleKey.Enter && keyPressed.Key != ConsoleKey.Escape);
                                if (keyPressed.Key == ConsoleKey.Escape)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("The message was not edited.");
                                    Console.ResetColor();
                                    
                                    return;
                                }
                                else if (keyPressed.Key == ConsoleKey.Enter)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Type whatever you like in order to replace the content of the message and press ENTER:\n");
                                    Console.ResetColor();



                                    string newContent = Console.ReadLine();


                                    db.Messages.Where(x => x.MessageId == messageIdForEdit).FirstOrDefault().Content = newContent;
                                    db.SaveChanges();

                                    Console.WriteLine();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Message with ID {messageIdForEdit} has been edited.");
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("\n\nPress any key to go back.");
                                    Console.ReadKey();
                                }
                            }
                        }
                    }
                }
            } while (!messageExists);
        }

        // Method of All Admins to view all the messages
        public void ViewAllMessages()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("======= Welcome Admin =======");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("======= Here you can view all the messages =======");
            Console.WriteLine();

            using (var db = new MyContext())
            {

                List<Message> allActiveMessages = new List<Message>();

                allActiveMessages = db.Messages.Include("Sender").Include("Recipient").Where(x => x.IsMessageActive == true).ToList();

                Console.WriteLine($"Total number of messages: {allActiveMessages.Count}"); // only the active messages

                foreach (var message in allActiveMessages)
                {
                    Console.Write($"Message ID: {message.MessageId}".PadRight(30));
                    Console.Write($"From {message.Sender.Username}".PadRight(20));
                    Console.Write($"To: {message.Recipient.Username}".PadRight(20));
                    Console.WriteLine($"Sent on: {message.DateCreated}");
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\n\nPress any key to go back."); 
                Console.ResetColor();
                Console.ReadKey();


            }
        }
    }
}
