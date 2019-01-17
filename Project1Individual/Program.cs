using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1Individual
{
    class Program
    {
        static void Main(string[] args)
        {
            /*try
            {
                throw new AccessViolationException();
                Console.WriteLine("here");
            }
            catch (Exception e)
            {
                Console.WriteLine("caught exception");
            }
            Console.WriteLine("goodbye"); */



            MenuManager menus = new MenuManager();
            DataBaseManager dBManager = new DataBaseManager();
            InputManager inputManager = new InputManager();
            MessageManager messageManager = new MessageManager();

            do
            {
                Enums.MainMenuOptions mainMenuChoice = menus.MainMenu(); //Run the Main Menu

                switch (mainMenuChoice)
                {

                    case Enums.MainMenuOptions.Login:
                        bool userExists = false;

                        bool userActive;


                        string usernameLogin;
                        menus.LoginMenu(); // Includes console clear and welcome message-to check that
                        usernameLogin = inputManager.InputUserName(); // Returns a string or null if ESC is pressed

                        if (usernameLogin != null) // if username is received
                        {
                            userExists = dBManager.DoesUsernameExist(usernameLogin); // checks if username exists in database
                            if (!userExists)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("The username you entered does not exist in the database. Please SignUp.");
                                Console.WriteLine("Press any key to go back to the Main Menu.");
                                Console.ResetColor();
                                Console.ReadKey();
                                break;
                            }
                            else // username exists in database, continue to ask for password
                            {
                                userActive = dBManager.IsUserActive(usernameLogin); // check if user is active

                                if (!userActive) // user not active
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"The user with username '{usernameLogin}' is no longer active.");
                                    Console.WriteLine("Press any key to go back");
                                    Console.ReadKey();
                                    Console.ResetColor();
                                    break;
                                }

                                Console.WriteLine($"\nWelcome {usernameLogin}!");
                                Console.WriteLine("\nPlease enter your password.");
                                int numberOfAttemps = 0; // Holds the number of attemps for password input
                                int maxNumberOfAttemps = 3;
                                string passwordLogin;
                                bool isPasswordCorrect = false;
                                while (!isPasswordCorrect && numberOfAttemps < maxNumberOfAttemps)
                                {
                                    passwordLogin = inputManager.InputLoginPassword(); 
                                    numberOfAttemps += 1;
                                    isPasswordCorrect = dBManager.IsPasswordCorrect(usernameLogin, passwordLogin);
                                    if (!isPasswordCorrect)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("Wrong password. Try again.");
                                        Console.ResetColor();
                                    }
                                }
                                if (!isPasswordCorrect && (numberOfAttemps == maxNumberOfAttemps))
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("Maximum number of attemps reached!");
                                    Console.ResetColor();
                                    Console.WriteLine("\nPress any key to go back.");
                                    Console.ReadKey();
                                    break; 
                                }
                                if (isPasswordCorrect)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Correct Password.");
                                    Console.ResetColor();

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("\nPress ESC to log out, Ctrl Q to exit or Enter to proceed to User Menu.");
                                    Console.ResetColor();

                                    Enums.ExitOptions userExitOption = inputManager.InputExitChoice();
                                    switch (userExitOption)
                                    {
                                        case Enums.ExitOptions.Esc:
                                            Console.Clear();
                                            Console.WriteLine("\nLogging out...");
                                            System.Threading.Thread.Sleep(700);
                                            break;

                                        case Enums.ExitOptions.CtrlQ:
                                            Console.Clear();
                                            Console.WriteLine("\nClosing application...");
                                            Environment.Exit(0);
                                            break;

                                        case Enums.ExitOptions.Enter:

                                            //dBManager.LogInUser(usernameLogin);   // to check that
                                            //bool islogged = dBManager.IsLoggedIn(usernameLogin);
                                            bool isLogged = true;

                                            //
                                            Enums.UserTypes userType = dBManager.GetUserType(usernameLogin); //check user type

                                            switch (userType)
                                            {
                                                case Enums.UserTypes.User:
                                                    do
                                                    {
                                                        Enums.UserMenuOptions userMenuOption = menus.UserMenu(usernameLogin);
                                                        switch (userMenuOption)
                                                        {
                                                            case Enums.UserMenuOptions.CreateNewMessage:
                                                                Console.Clear();
                                                                Console.WriteLine("======= Create new Message =======");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Please type the username of the recipient of the message:\n");
                                                                string recipient = inputManager.InputUserName();
                                                            


                                                                if (recipient is null) //if ESC is pressed
                                                                {
                                                                    break;
                                                                }

                                                                // check if recipient username exists in database
                                                                bool recipientExists = dBManager.DoesUsernameExist(recipient);
                                                                if (!recipientExists)
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                                                    Console.WriteLine();
                                                                    Console.WriteLine($"A user with username '{recipient}' does not exist.");
                                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                                    Console.WriteLine("\nPress any key to go back to the user menu");
                                                                    Console.ResetColor();
                                                                    Console.ReadKey();
                                                                }
                                                                else // the recipient exists. Go on to create and send message
                                                                {
                                                                    // check if recipient is active
                                                                    bool recipientActive = dBManager.IsUserActive(recipient);
                                                                    if (!recipientActive)
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                        Console.WriteLine("\nThe recipient you chose is no longer active.");
                                                                        Console.WriteLine("Try sending a message to another user.");
                                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                    else
                                                                    {
                                                                        messageManager.CreateMessage(usernameLogin,recipient);
                                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }

                                                                }
                                                                break;

                                                            case Enums.UserMenuOptions.Inbox:
                                                                messageManager.ShowInbox(usernameLogin);
                                                                break;

                                                            case Enums.UserMenuOptions.SentMessages:
                                                                messageManager.ShowSentMessages(usernameLogin);
                                                                break;

                                                            case Enums.UserMenuOptions.Info:
                                                                dBManager.GetUserInfo(usernameLogin);
                                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                                Console.WriteLine("\nPress any key to go back");
                                                                Console.ResetColor();
                                                                Console.ReadKey();
                                                                break;

                                                            case Enums.UserMenuOptions.ExitToMain:
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nLogging out...");
                                                                System.Threading.Thread.Sleep(700);
                                                                break;
                                                            case Enums.UserMenuOptions.Quit:
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nClosing application...");
                                                                Environment.Exit(0);
                                                                break;
                                                        }
                                                    } while (isLogged);
                                                    break;

                                                case Enums.UserTypes.JuniorAdmin:

                                                    do
                                                    {
                                                        Enums.JuniorAdminMenuOptions juniorAdminMenuOption = menus.JuniorAdminMenu(usernameLogin);

                                                        switch (juniorAdminMenuOption)
                                                        {
                                                            case Enums.JuniorAdminMenuOptions.CreateNewMessage:

                                                                Console.Clear();
                                                                Console.WriteLine("======= Create new Message =======");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Please type the username of the recipient of the message:\n");
                                                                string recipient = inputManager.InputUserName();

                                                                if (recipient is null) //if ESC is pressed
                                                                {
                                                                    break;
                                                                }

                                                                // check if recipient username exists in database
                                                                bool recipientExists = dBManager.DoesUsernameExist(recipient);
                                                                if (!recipientExists)
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                                                    Console.WriteLine();
                                                                    Console.WriteLine($"A user with username '{recipient}' does not exist.");
                                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                                    Console.WriteLine("\nPress any key to go back to the user menu");
                                                                    Console.ResetColor();
                                                                    Console.ReadKey();
                                                                }
                                                                else // the recipient exists. Go on to create and send message
                                                                {
                                                                    // check if recipient is active
                                                                    bool recipientActive = dBManager.IsUserActive(recipient);
                                                                    if (!recipientActive)
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                        Console.WriteLine("\nThe recipient you chose is no longer active.");
                                                                        Console.WriteLine("Try sending a message to another user.");
                                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                    else
                                                                    {
                                                                        messageManager.CreateMessage(usernameLogin, recipient);
                                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                }
                                                                break;

                                                            case Enums.JuniorAdminMenuOptions.Inbox:

                                                                messageManager.ShowInbox(usernameLogin);
                                                                break;

                                                            case Enums.JuniorAdminMenuOptions.SentMessages:

                                                                messageManager.ShowSentMessages(usernameLogin);
                                                                break;

                                                            case Enums.JuniorAdminMenuOptions.Info:

                                                                dBManager.GetUserInfo(usernameLogin);
                                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                                Console.WriteLine("\nPress any key to go back");
                                                                Console.ResetColor();
                                                                Console.ReadKey();
                                                                break;

                                                            case Enums.JuniorAdminMenuOptions.ViewUserInfo:

                                                                dBManager.ViewUserInfo();
                                                                break;

                                                            case Enums.JuniorAdminMenuOptions.ViewUserMessages:

                                                                messageManager.ViewUserMessages();
                                                                break;

                                                            case Enums.JuniorAdminMenuOptions.ViewAllMessages:

                                                                messageManager.ViewAllMessages();
                                                                break;

                                                            case Enums.JuniorAdminMenuOptions.EditMessages:

                                                                messageManager.EditMessage();
                                                                break;

                                                            case Enums.JuniorAdminMenuOptions.ExitToMain:
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nGoodbye Junior...");
                                                                System.Threading.Thread.Sleep(700);
                                                                break;

                                                            case Enums.JuniorAdminMenuOptions.Quit:
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nClosing application...");
                                                                Environment.Exit(0);
                                                                break;
                                                        }

                                                    } while (isLogged);

                                                    break;


                                                case Enums.UserTypes.MasterAdmin:

                                                    do
                                                    {
                                                        Enums.MasterAdminMenuOptions masterAdminMenuOption = menus.MasterAdminMenu(usernameLogin);

                                                        switch (masterAdminMenuOption)
                                                        {
                                                            case Enums.MasterAdminMenuOptions.CreateNewMessage:

                                                                Console.Clear();
                                                                Console.WriteLine("======= Create new Message =======");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Please type the username of the recipient of the message:\n");
                                                                string recipient = inputManager.InputUserName();

                                                                if (recipient is null)
                                                                {
                                                                    break;
                                                                }

                                                                // check if recipient username exists in database
                                                                bool recipientExists = dBManager.DoesUsernameExist(recipient);
                                                                if (!recipientExists)
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                                                    Console.WriteLine();
                                                                    Console.WriteLine($"A user with username '{recipient}' does not exist.");
                                                                    Console.ForegroundColor = ConsoleColor.Blue;
                                                                    Console.WriteLine("\nPress any key to go back to the user menu");
                                                                    Console.ResetColor();
                                                                    Console.ReadKey();
                                                                }
                                                                else // the recipient exists. Go on to create and send message
                                                                {
                                                                    // check if recipient is active
                                                                    bool recipientActive = dBManager.IsUserActive(recipient);
                                                                    if (!recipientActive)
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                        Console.WriteLine("\nThe recipient you chose is no longer active.");
                                                                        Console.WriteLine("Try sending a message to another user.");
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                    else
                                                                    {
                                                                        messageManager.CreateMessage(usernameLogin, recipient);
                                                                        Console.ForegroundColor = ConsoleColor.Blue;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                }
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.Inbox:

                                                                messageManager.ShowInbox(usernameLogin);
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.SentMessages:

                                                                messageManager.ShowSentMessages(usernameLogin);
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.Info:

                                                                dBManager.GetUserInfo(usernameLogin);
                                                                Console.ForegroundColor = ConsoleColor.Cyan;
                                                                Console.WriteLine("\nPress any key to go back");
                                                                Console.ResetColor();
                                                                Console.ReadKey();
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.ViewUserInfo:

                                                                dBManager.ViewUserInfo();
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.ViewUserMessages:

                                                                messageManager.ViewUserMessages();
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.ViewAllMessages:

                                                                messageManager.ViewAllMessages();
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.EditMessages:

                                                                messageManager.EditMessage();
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.DeleteMessages:

                                                                messageManager.DeleteMessage();
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.ExitToMain:
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nGoodbye Admin...");
                                                                System.Threading.Thread.Sleep(700);
                                                                break;

                                                            case Enums.MasterAdminMenuOptions.Quit:
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nClosing application...");
                                                                Environment.Exit(0);
                                                                break;
                                                        }

                                                    } while (isLogged);

                                                    break;

                                                case Enums.UserTypes.SuperAdmin:

                                                    do
                                                    {
                                                        Enums.SuperAdminMenuOptions superAdminMenuOption = menus.SuperAdminMenu(usernameLogin);

                                                        switch (superAdminMenuOption)
                                                        {
                                                            case Enums.SuperAdminMenuOptions.CreateNewMessage:

                                                                Console.Clear();
                                                                Console.WriteLine("======= Create new Message =======");
                                                                Console.WriteLine();
                                                                Console.WriteLine("Please type the username of the recipient of the message:\n");
                                                                string recipient = inputManager.InputUserName();

                                                                if (recipient is null) //if ESC is pressed
                                                                {
                                                                    break;
                                                                }

                                                                // check if recipient username exists in database
                                                                bool recipientExists = dBManager.DoesUsernameExist(recipient);
                                                                if (!recipientExists)
                                                                {
                                                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                                                    Console.WriteLine();
                                                                    Console.WriteLine($"A user with username '{recipient}' does not exist.");
                                                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                                                    Console.WriteLine("\nPress any key to go back to the user menu");
                                                                    Console.ResetColor();
                                                                    Console.ReadKey();
                                                                }
                                                                else // the recipient exists. Go on to create and send message
                                                                {
                                                                    // check if recipient is active
                                                                    bool recipientActive = dBManager.IsUserActive(recipient);
                                                                    if (!recipientActive)
                                                                    {
                                                                        Console.ForegroundColor = ConsoleColor.Red;
                                                                        Console.WriteLine("\nThe recipient you chose is no longer active.");
                                                                        Console.WriteLine("Try sending a message to another user.");
                                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                    else
                                                                    {
                                                                        messageManager.CreateMessage(usernameLogin, recipient);
                                                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                                                        Console.WriteLine("\nPress any key to go back to the user menu");
                                                                        Console.ResetColor();
                                                                        Console.ReadKey();
                                                                    }
                                                                }
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.Inbox:

                                                                messageManager.ShowInbox(usernameLogin);
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.SentMessages:

                                                                messageManager.ShowSentMessages(usernameLogin);
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.Info:

                                                                dBManager.GetUserInfo(usernameLogin);
                                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                                Console.WriteLine("\nPress any key to go back");
                                                                Console.ResetColor();
                                                                Console.ReadKey();
                                                                break;
                                                            //==========================================================================================================================================//
                                                            case Enums.SuperAdminMenuOptions.CreateNewUser:

                                                                dBManager.CreateNewUser();
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.DeleteUser:

                                                                dBManager.DeleteUser();
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.ActivateUser:

                                                                dBManager.ActivateUser();
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.EditUserType:

                                                                dBManager.EditUserType();
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.ViewUserInfo:

                                                                dBManager.ViewUserInfo();
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.ViewUserMessages:

                                                                messageManager.ViewUserMessages();
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.ViewAllMessages:

                                                                messageManager.ViewAllMessages();
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.DeleteMessages:

                                                                messageManager.DeleteMessage();
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.EditMessages:

                                                                messageManager.EditMessage();
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.ExitToMain:
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nGoodbye Master...");
                                                                System.Threading.Thread.Sleep(700);
                                                                break;

                                                            case Enums.SuperAdminMenuOptions.Quit:
                                                                Console.Clear();
                                                                isLogged = false;
                                                                Console.WriteLine("\nClosing application...");
                                                                Environment.Exit(0);
                                                                break;
                                                        }

                                                    } while (isLogged);

                                                    break;
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                        else // Break to main menu if username is null after ESC is pressed
                        {
                            break;
                        }
                        break;

                    case Enums.MainMenuOptions.SignUp:
                        bool itExists = false;
                        string usernameSignup;
                        do
                        {
                            menus.SignUpMenu(); 
                            usernameSignup = inputManager.InputUserName(); 
                            if (usernameSignup is null)
                            {
                                break;
                            }
                            itExists = dBManager.DoesUsernameExist(usernameSignup); // Check if username already exists in database
                            if (itExists)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("The username you entered already exists. Please choose another.");
                                Console.ResetColor();
                                Console.ReadKey();
                            }
                        } while (itExists);
                        if (usernameSignup != null) // username is null if ESC is pressed
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("The username you entered is not taken. Nice choice!");
                            Console.ResetColor();

                            Console.WriteLine();
                            string password = inputManager.InputPassword();
                            dBManager.AddUser(usernameSignup, password); // add new user to database
                            Console.WriteLine("\nPress any key to go back to Main Menu.");
                            Console.ReadKey();
                        }
                        break;

                    case Enums.MainMenuOptions.Info:
                        dBManager.GetInfo();
                        break;

                    case Enums.MainMenuOptions.Exit:
                        Console.Clear();
                        Console.WriteLine("\nClosing application...");
                        Environment.Exit(0);
                        break;
                }
            } while (true);
        
            
            }
    }
}
