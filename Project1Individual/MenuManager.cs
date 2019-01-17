using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1Individual
{
    class MenuManager
    {
        public int ScrollMenu(string header, string[] menuItems)
        {
            int currentOption = 0;
            ConsoleKeyInfo keyPressed;

            do
            {
                Console.Clear();
                Console.WriteLine(header);
                Console.WriteLine();

                for (int c = 0; c < menuItems.Length; c++)
                {
                    if (currentOption == c)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(">> ");
                        Console.WriteLine(menuItems[c]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"    { menuItems[c]}");
                    }

                }
                keyPressed = Console.ReadKey(true);


                if (keyPressed.Key == ConsoleKey.DownArrow)
                {
                    currentOption++;
                    if (currentOption > menuItems.Length - 1) //Go back to top
                    {
                        currentOption = 0;
                    }
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow)
                {
                    currentOption--;
                    if (currentOption < 0) // Go back to bottom
                    {
                        currentOption = (menuItems.Length - 1);
                    }
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    return currentOption;
                }


            } while (true);
        }
            // Main Menu
            public  Enums.MainMenuOptions MainMenu()
            {
                while (true) 
                {
                    string header = "================== Welcome! ================== \n";
                    int option = ScrollMenu(header, Enum.GetNames(typeof(Enums.MainMenuOptions))); 
                                                                                  
                    switch (option)
                    {
                        case 0:
                            return Enums.MainMenuOptions.Login;
                        case 1:
                            return Enums.MainMenuOptions.SignUp;
                        case 2:
                            return Enums.MainMenuOptions.Info;
                        case 3:
                            return Enums.MainMenuOptions.Exit;
                    }
                }
            }

            // Menu User //SIMPLE USER
            public Enums.UserMenuOptions UserMenu(string username)
            {
                while (true)
                {
                    string header = $"======= Welcome to the User Menu {username}! =======\n\nPlease choose an option.\n";
                    int option = ScrollMenu(header, Enum.GetNames(typeof(Enums.UserMenuOptions))); 
                    switch (option)
                    {
                        case 0:
                            return Enums.UserMenuOptions.CreateNewMessage;
                        case 1:
                            return Enums.UserMenuOptions.Inbox;
                        case 2:
                            return Enums.UserMenuOptions.SentMessages;
                        case 3:
                            return Enums.UserMenuOptions.Info;
                        case 4:
                            return Enums.UserMenuOptions.ExitToMain;
                        case 5:
                            return Enums.UserMenuOptions.Quit;
                    }
                }

            }

            // Menu Super Admin
            public Enums.SuperAdminMenuOptions SuperAdminMenu(string username)
            {
                while (true)
                {
                    string header = $"======= Welcome to the SUPER ADMIN Menu =======\n\nPlease choose an option.\n";
                    int option = ScrollMenu(header, Enum.GetNames(typeof(Enums.SuperAdminMenuOptions))); 
                    switch (option)
                    {
                        case 0:
                            return Enums.SuperAdminMenuOptions.CreateNewMessage;
                        case 1:
                            return Enums.SuperAdminMenuOptions.Inbox;
                        case 2:
                            return Enums.SuperAdminMenuOptions.SentMessages;
                        case 3:
                            return Enums.SuperAdminMenuOptions.Info;
                        case 4:
                            return Enums.SuperAdminMenuOptions.CreateNewUser;
                        case 5:
                            return Enums.SuperAdminMenuOptions.DeleteUser;
                        case 6:
                            return Enums.SuperAdminMenuOptions.ActivateUser;
                        case 7:
                            return Enums.SuperAdminMenuOptions.EditUserType;
                        case 8:
                            return Enums.SuperAdminMenuOptions.ViewUserInfo;
                        case 9:
                            return Enums.SuperAdminMenuOptions.ViewUserMessages;
                        case 10:
                            return Enums.SuperAdminMenuOptions.ViewAllMessages;
                        case 11:
                            return Enums.SuperAdminMenuOptions.DeleteMessages;
                        case 12:
                            return Enums.SuperAdminMenuOptions.EditMessages;
                        case 13:
                            return Enums.SuperAdminMenuOptions.ExitToMain;
                        case 14:
                            return Enums.SuperAdminMenuOptions.Quit;
                    }

                }
            }

            // Menu Junior Admin
            public Enums.JuniorAdminMenuOptions JuniorAdminMenu(string username)
            {
                while (true)
                {
                    string header = $"======= Welcome to the Junior ADMIN Menu =======\n\nPlease choose an option.\n";
                    int option = ScrollMenu(header, Enum.GetNames(typeof(Enums.JuniorAdminMenuOptions))); 
                    switch (option)
                    {
                        case 0:
                            return Enums.JuniorAdminMenuOptions.CreateNewMessage;
                        case 1:
                            return Enums.JuniorAdminMenuOptions.Inbox;
                        case 2:
                            return Enums.JuniorAdminMenuOptions.SentMessages;
                        case 3:
                            return Enums.JuniorAdminMenuOptions.Info;
                        case 4:
                            return Enums.JuniorAdminMenuOptions.ViewUserInfo;
                        case 5:
                            return Enums.JuniorAdminMenuOptions.ViewUserMessages;
                        case 6:
                            return Enums.JuniorAdminMenuOptions.ViewAllMessages;
                        case 7:
                            return Enums.JuniorAdminMenuOptions.EditMessages;
                        case 8:
                            return Enums.JuniorAdminMenuOptions.ExitToMain;
                        case 9:
                            return Enums.JuniorAdminMenuOptions.Quit;
                    }

                }
            }

            // Menu Master Admin
            public Enums.MasterAdminMenuOptions MasterAdminMenu(string username)
            {
                while (true)
                {
                    string header = $"======= Welcome to the Master ADMIN Menu =======\n\nPlease choose an option.\n";
                    int option = ScrollMenu(header, Enum.GetNames(typeof(Enums.MasterAdminMenuOptions))); 
                    switch (option)
                    {
                        case 0:
                            return Enums.MasterAdminMenuOptions.CreateNewMessage;
                        case 1:
                            return Enums.MasterAdminMenuOptions.Inbox;
                        case 2:
                            return Enums.MasterAdminMenuOptions.SentMessages;
                        case 3:
                            return Enums.MasterAdminMenuOptions.Info;
                        case 4:
                            return Enums.MasterAdminMenuOptions.ViewUserInfo;
                        case 5:
                            return Enums.MasterAdminMenuOptions.ViewUserMessages;
                        case 6:
                            return Enums.MasterAdminMenuOptions.ViewAllMessages;
                        case 7:
                            return Enums.MasterAdminMenuOptions.EditMessages;
                        case 8:
                            return Enums.MasterAdminMenuOptions.DeleteMessages;
                        case 9:
                            return Enums.MasterAdminMenuOptions.ExitToMain;
                        case 10:
                            return Enums.MasterAdminMenuOptions.Quit;
                    }

                }
            }





            // Menu SignUp
            public void SignUpMenu()
            {
                Console.Clear();
                Console.WriteLine("========= Welcome to the Sign Up Menu! =========\n");
                Console.WriteLine("Seems like you are a new user!\n");
                Console.WriteLine("Please enter the username you would like to have or press ESC to go back:");

            }

            // Menu Login
            public void LoginMenu()
            {
                Console.Clear();
                Console.WriteLine("========= Welcome to the Login Menu! =========\n");
                Console.WriteLine("Please enter your username or press ESC to go back:");
            }

            // Scroll Inbox Menu 
            public void ScrollInboxMenu(string username, List<Message> messages) // list of inbox messages
            {
                using (var db = new MyContext())
                {
                    // arrays to hold messages information
                    int[] messageIds = messages.Select(x => x.MessageId).ToArray(); 
                    string[] subjects = messages.Select(x => x.Subject).ToArray();
                    string[] contents = messages.Select(x => x.Content).ToArray();
                    string[] senders = messages.Select(x => x.Sender.Username).ToArray();
                    string[] date = messages.Select(x => x.DateCreated.ToString()).ToArray();

                    int currentOption = 0; 
                    ConsoleKeyInfo keyPressed; 
                    do
                    {

                        Console.Clear();
                        Console.WriteLine(); 
                        Console.WriteLine($"======= Inbox of {username} =======");
                        Console.WriteLine();
                        for (int c = 0; c < messageIds.Length; c++)
                        {
                            if (currentOption == c)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(">> ");
                                Console.WriteLine($"{messageIds[c]}: {subjects[c]}");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine($"   {messageIds[c]}: {subjects[c]}");

                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n\n\n\nPress ESC to go back.");
                        Console.ResetColor();
                        keyPressed = Console.ReadKey(true);

                        if (keyPressed.Key == ConsoleKey.DownArrow)
                        {
                            currentOption++;
                            if (currentOption > messageIds.Length - 1) //Go back to top
                            {
                                currentOption = 0;
                            }
                        }
                        else if (keyPressed.Key == ConsoleKey.UpArrow)
                        {
                            currentOption--;
                            if (currentOption < 0) // Go back to bottom
                            {
                                currentOption = (messageIds.Length - 1);
                            }
                        }
                        else if (keyPressed.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            Console.WriteLine($"Message ID   : {messageIds[currentOption]}");
                            Console.WriteLine($"From         : {senders[currentOption]}");
                            Console.WriteLine($"To           : {username}");
                            Console.WriteLine($"Received on  : {date[currentOption]}");

                            Console.WriteLine();
                            Console.WriteLine("\nBody of message:\n");
                            Console.WriteLine(contents[currentOption]);

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Press any key to go back.");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        else if (keyPressed.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    } while (true);
                }
            }

            // Scroll Sent Menu 
            public void ScrollSentMenu(string username, List<Message> messages) 
            {
                using (var db = new MyContext())
                {
                    
                    int[] messageIds = messages.Select(x => x.MessageId).ToArray(); 
                    string[] subjects = messages.Select(x => x.Subject).ToArray();
                    string[] contents = messages.Select(x => x.Content).ToArray();
                    string[] recipients = messages.Select(x => x.Recipient.Username).ToArray();
                    string[] date = messages.Select(x => x.DateCreated.ToString()).ToArray();

                    int currentOption = 0; 
                    ConsoleKeyInfo keyPressed; 
                    do
                    {
                        Console.Clear();
                        Console.WriteLine(); 
                        Console.WriteLine($"======= Sent Messages from {username} =======");
                        Console.WriteLine();
                        for (int c = 0; c < messageIds.Length; c++)
                        {
                            if (currentOption == c)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write(">> ");
                                Console.WriteLine($"{messageIds[c]}: {subjects[c]}");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.WriteLine($"   {messageIds[c]}: {subjects[c]}");

                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\n\n\n\nPress ESC to go back.");
                        Console.ResetColor();
                        keyPressed = Console.ReadKey(true);

                        if (keyPressed.Key == ConsoleKey.DownArrow)
                        {
                            currentOption++;
                            if (currentOption > messageIds.Length - 1) //Go back to top
                            {
                                currentOption = 0;
                            }
                        }
                        else if (keyPressed.Key == ConsoleKey.UpArrow)
                        {
                            currentOption--;
                            if (currentOption < 0) // Go back to bottom
                            {
                                currentOption = (messageIds.Length - 1);
                            }
                        }
                        else if (keyPressed.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            Console.WriteLine($"Message ID   : {messageIds[currentOption]}");
                            Console.WriteLine($"From         : {username}");
                            Console.WriteLine($"To           : {recipients[currentOption]}");
                            Console.WriteLine($"Sent on      : {date[currentOption]}");

                            Console.WriteLine();
                            Console.WriteLine("\nBody of message:\n");
                            Console.WriteLine(contents[currentOption]);

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Press any key to go back.");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        else if (keyPressed.Key == ConsoleKey.Escape)
                        {
                            break;
                        }
                    } while (true);
                }
            }



        }
    }

