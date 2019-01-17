using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1Individual
{
    public class DataBaseManager
    {

        InputManager DataBaseInputManager = new InputManager(); 
        // General information from database
        public void GetInfo()
        {
            using (var db = new MyContext())
            {
                Console.Clear();
                
                Console.WriteLine("Searching Database..."); 
                Console.ResetColor();
                
                int numberOfActiveUsers = db.Users.Where(x => x.IsUserActive == true).Count();
                int numberOfDeletedUsers = db.Users.Where(x => x.IsUserActive == false).Count();
                
                int numberOfActiveMessages = db.Messages.Where(x => x.IsMessageActive == true).Count();
                int numberOfDeletedMessages = db.Messages.Where(x => x.IsMessageActive == false).Count();

                Console.Clear();
                Console.WriteLine("======= Database Information =======");
                Console.WriteLine();
                Console.WriteLine($"Number of existing users so far   : {numberOfActiveUsers}");
                Console.WriteLine($"Number of deleted users           : {numberOfDeletedUsers}");
                Console.WriteLine();
                Console.WriteLine($"Number of existing messages so far: {numberOfActiveMessages}");
                Console.WriteLine($"Number of deleted messages        : {numberOfDeletedMessages}");
                Console.WriteLine();

                // Print the existing usernames in the database
                Console.WriteLine("Existing active usernames in the database:\n");
                var allActiveUsers = db.Users.Where(x => x.IsUserActive == true).ToList();
                foreach (var user in allActiveUsers)
                {
                    Console.Write($"username: {user.Username}".PadRight(30));
                    Console.Write($"{(Enums.UserTypes)user.UserType}".PadRight(20));
                    Console.WriteLine($"created on: {user.DateCreated}");
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n\n\nPress any key to go back."); 
                Console.ResetColor();
                Console.ReadKey();

            }
        }

        // Method to fetch information about user from database
        public void GetUserInfo(string username)
        {
            using (var db = new MyContext())
            {
                Console.Clear();
                var user = db.Users.Where(x => x.Username == username).First();
                var userSentMessages = db.Messages.Include("Sender")
                    .Where(x => x.Sender.Username == username && x.IsMessageActive == true).ToList();
                
                var userReceivedMessages = db.Messages.Include("Recipient")
                    .Where(x => x.Recipient.Username == username && x.IsMessageActive == true).ToList();
                Console.WriteLine($"======= Information about {username}'s profile =======");
                Console.WriteLine();
                Console.WriteLine($"Profile created on: {user.DateCreated}");
                Console.WriteLine();
                Console.WriteLine($"Number of sent messages for {username}:        {userSentMessages.Count}");
                Console.WriteLine($"Number of received messages for {username}:    {userReceivedMessages.Count}");

                Console.WriteLine();
                Console.WriteLine($"User type: {(Enums.UserTypes)user.UserType}");

                if (user.IsUserActive)
                {
                    Console.WriteLine($"Status   : Active");
                }
                else
                {
                    Console.WriteLine($"Status   : Deleted");
                }
            }
        }


        // Method to check if username exists in database
        public bool DoesUsernameExist(string username)
        {
            bool itExists = false;
            using (var db = new MyContext())
            {
                if (db.Users.Any(x => x.Username == username))
                {
                    itExists = true;
                }
            }
            return itExists;
        }

        // Method to check if username is active
        public bool IsUserActive(string username)
        {
            bool isActive = false;
            using (var db = new MyContext())
            {
                if (db.Users.Where(x => x.Username == username).FirstOrDefault().IsUserActive == true)
                {
                    isActive = true;
                }
            }
            return isActive;

        }

        // Method to return usertype of username
        public Enums.UserTypes GetUserType(string username)
        {
            using (var db = new MyContext())
            {
                Enums.UserTypes userType = (Enums.UserTypes)db.Users.Where(x => x.Username == username).FirstOrDefault().UserType;
                return userType;
            }
        }

        // Method to add new user to database
        public void AddUser(string username, string password, int type = 1)
        {
            using (var db = new MyContext())
            {
                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Creating new user profile.....Please wait...");
                db.Users.Add(new User(username, password, type));
                db.SaveChanges();
                Console.WriteLine($"\nNew user profile with username '{username}' has been created!");
                Console.ResetColor();
            }
        }

        // Method to check if password is correct, given the username
        public bool IsPasswordCorrect(string username, string password)
        {
            using (var db = new MyContext())
            {
                bool passwordCorrect = false;
                if (db.Users.Any(x => x.Username == username && x.Password == password))
                {
                    passwordCorrect = true;
                }
                return passwordCorrect;
            }
        }

        // Method of Super Admin to create new user
        public void CreateNewUser()
        {
            bool userExists;
            string usernameForNewUser;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("======= Welcome MasterAdmin! =======");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("======= You can create a new user! =======");
                Console.WriteLine();
                Console.WriteLine("Choose a username for the new user:");
                usernameForNewUser = DataBaseInputManager.InputUserName(); 
                if (usernameForNewUser is null) 
                {
                    return;
                }
                userExists = DoesUsernameExist(usernameForNewUser); // Check if username already exists in database
                if (userExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The username you entered already exists.\nPlease choose another.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            } while (userExists);

            Console.WriteLine();
            string passwordForNewUser = DataBaseInputManager.InputPassword();

            Console.WriteLine();
            Console.WriteLine("Please choose a type for the new user.");
            Console.WriteLine("Press 1 for simple user, 2 for Junior Admin, 3 for Master Admin:");

            
            int userType;
            bool successType = false;
            do
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out userType))
                {
                    if (userType != 1 && userType != 2 && userType != 3)
                    {
                        Console.WriteLine("That type does not exist. Please try again.");
                    }
                    else
                    {
                        Console.WriteLine($"\nYou chose user of type {userType}.");
                        successType = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Input. Please try again.");

                }
            } while (!successType);

            

            AddUser(usernameForNewUser, passwordForNewUser, userType); // create new user and save to database

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nPress any key to go back to Super Admin Menu.");
            Console.ResetColor();
            Console.ReadKey();
        }

        // Method of Super Admin to delete user
        public void DeleteUser()
        {
            bool userExists;
            string usernameForDelete;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("======= Welcome Super Admin =======");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("======= You can delete a user! =======");
                Console.WriteLine();
                Console.WriteLine("Choose the username of the user you would like to delete:");
                usernameForDelete = DataBaseInputManager.InputUserName(); // username is Null if ESC is pressed during input
                if (usernameForDelete is null) // is ESC is pressed return to admin menu
                {
                    return;
                }
                if (usernameForDelete == "admin")
                {
                    Console.WriteLine("You are going to delete yoursef!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }
                userExists = DoesUsernameExist(usernameForDelete);
                if (!userExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("We are so sorry Admin but the username you entered does not exist.\nPlease choose another user to delete.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    bool userActive = IsUserActive(usernameForDelete);
                    if(!userActive)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"The user with username '{usernameForDelete}' is no longer active.\n");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                
                    else
                    {
                        using (var db = new MyContext())
                        {
                            db.Users.Where(x => x.Username == usernameForDelete).FirstOrDefault().IsUserActive = false;
                            db.SaveChanges();
                        }
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"User '{usernameForDelete}' is no longer active.");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                }
            } while (!userExists);
        }

        // Method of Super Admin to activate previously deleted user
        public void ActivateUser()
        {
            bool userExists;
            string usernameToActivate;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("======= Welcome Super Admin =======");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("======= You can activate a user! =======");
                Console.WriteLine();
                Console.WriteLine("Choose the username of the user you would like to activate:");
                usernameToActivate = DataBaseInputManager.InputUserName(); 
                if (usernameToActivate is null) 
                {
                    return;
                }
                userExists = DoesUsernameExist(usernameToActivate); // Check if username already exists in database
                if (!userExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The username you entered does not exist.\nPlease choose another user to activate.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    bool userActive = IsUserActive(usernameToActivate); // check if user is active
                    if (userActive)
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"The user with username '{usernameToActivate}' is already active.");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                    else
                    {
                        using (var db = new MyContext())
                        {
                            db.Users.Where(x => x.Username == usernameToActivate).FirstOrDefault().IsUserActive = true;
                            db.SaveChanges();
                        }
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"User '{usernameToActivate}' is now active Admin.");
                        
                        Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                        Console.ResetColor();
                        Console.ReadKey();
                    }
                }
            } while (!userExists);
        }

        // Method of Super Admin to edit the type of user
        public void EditUserType()
        {
            bool userExists;
            string usernameToEdit;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("======= Welcome Super Admin =======");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("======= You can edit the type of a user! =======");
                Console.WriteLine();
                Console.WriteLine("Choose the username of the user you would like to edit:");
                usernameToEdit = DataBaseInputManager.InputUserName(); 
                if (usernameToEdit is null) 
                {
                    return;
                }
                if (usernameToEdit == "admin")
                {
                    Console.WriteLine("Cannot do that! Sorry!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }
                userExists = DoesUsernameExist(usernameToEdit); // Check if username already exists in database
                if (!userExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The username you entered does not exist.\nPlease choose another user to edit.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Please choose a type for user '{usernameToEdit}'.");
                    Console.WriteLine("Press 1 for simple user, 2 for Junior Admin, 3 for Master Admin:");


                    int userType;
                    bool successType = false;
                    do
                    {
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out userType))
                        {
                            if (userType != 1 && userType != 2 && userType != 3)
                            {
                                Console.WriteLine("That type does not exist. Please try again.");
                            }
                            else
                            {
                                Console.WriteLine($"\nYou chose type {userType}.");
                                successType = true;
                            }
                        }
                        else 
                        {
                            Console.WriteLine("Invalid Input. Please try again.");

                        }
                    } while (!successType);

                    
                    using (var db = new MyContext())
                    {
                        db.Users.Where(x => x.Username == usernameToEdit).FirstOrDefault().UserType = userType;
                        db.SaveChanges();
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"User '{usernameToEdit}' is now user of type {userType}: {(Enums.UserTypes)userType}.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\nPress any key to go back to Super Admin Menu.");
                    Console.ResetColor();
                    Console.ReadKey();

                }
            } while (!userExists);
        }

                // Method of all Admins to view information about a user

                public void ViewUserInfo()

                {
                bool userExists;
                string usernameToViewInfo;
                do
                {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("======= Welcome Admin! =======");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("======= Here you can view the information about a user =======");
                Console.WriteLine();
                Console.WriteLine("Choose the username of the user you would like to view:");
                usernameToViewInfo = DataBaseInputManager.InputUserName(); 
                if (usernameToViewInfo is null) 
                {
                    return;
                }
                userExists = DoesUsernameExist(usernameToViewInfo); 
                if (!userExists)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The username you entered does not exist.\nPlease choose another user to view.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else
                {
                    GetUserInfo(usernameToViewInfo);

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nPress any key to go back");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            } while (!userExists);
        }

        // Method to check if message with messageID exists in database
        public bool DoesMessageExist(int messageId)
        {
            bool itExists = false;
            using (var db = new MyContext())
            {
                if (db.Messages.Any(x => x.MessageId == messageId))
                {
                    itExists = true;
                }
            }
            return itExists;
        }

        // Method to check if message is active
        public bool IsMessageActive(int messageId)
        {
            bool isActive = false;
            using (var db = new MyContext())
            {
                if (db.Messages.Where(x => x.MessageId == messageId).FirstOrDefault().IsMessageActive == true)
                {
                    isActive = true;
                }
            }
            return isActive;

        }



    }
}
