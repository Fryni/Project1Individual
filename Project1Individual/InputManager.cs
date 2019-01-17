using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1Individual
{
    public class InputManager
    {
        public string InputUserName()
        {
            string username;

            username = String.Empty;
            ConsoleKeyInfo keyPressed;

            do
            {

                keyPressed = Console.ReadKey(true);

                while (keyPressed.Key == ConsoleKey.Enter && username.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("\n Username cannot be of zero length. Please enter a valid username!");
                    Console.ResetColor();
                    keyPressed = Console.ReadKey(true);

                }

                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    return null;

                }
                if (!char.IsControl(keyPressed.KeyChar)) //No control characters
                {
                    username += keyPressed.KeyChar;
                    Console.Write(keyPressed.KeyChar);
                }
                else
                {
                    if (keyPressed.Key == ConsoleKey.Backspace && username.Length > 0)
                    {
                        username = username.Substring(0, (username.Length - 1));
                        Console.Write("\b \b"); 
                    }
                }
            }
            while (keyPressed.Key != ConsoleKey.Enter); // Stop receiving keys once Enter is pressed.
            Console.WriteLine();

            return username;
        }

        public string InputPassword()
        {
            string password = String.Empty;
            int MinPassLength = 5;
            Console.WriteLine($"Please enter your preferred password [minimum {MinPassLength} characters long] : ");
            ConsoleKeyInfo keyPressed;
            while (password.Length < MinPassLength)
            {
                do
                {
                    keyPressed = Console.ReadKey(true);

                    while (keyPressed.Key == ConsoleKey.Enter && password.Length == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("\n Password cannot be of zero length! Please enter a valid password.");
                        Console.ResetColor();
                        keyPressed = Console.ReadKey(true);
                    }

                    if ((!char.IsControl(keyPressed.KeyChar)))
                    {
                        password += keyPressed.KeyChar;
                        Console.Write("*");
                    }
                    else
                    {
                        if (keyPressed.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                    }
                }
                while (keyPressed.Key != ConsoleKey.Enter);
                if (password.Length < MinPassLength) //Checked!
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine($"\nPassword must be at least {MinPassLength} characters long.\nPlease try again.");
                    Console.ResetColor();
                    password = "";
                }
            }
            Console.WriteLine();
            return password;
        }

        // Method to receive password during Login. 
        public string InputLoginPassword()
        {
            string password;
            ConsoleKeyInfo keyPressed;
            password = String.Empty;
            do
            {
                keyPressed = Console.ReadKey(true);

                while (keyPressed.Key == ConsoleKey.Enter && password.Length == 0)

                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPassword cannot be of zero length! Please enter a valid password.");
                    Console.ResetColor();
                    keyPressed = Console.ReadKey(true);
                }

                if ((!char.IsControl(keyPressed.KeyChar)))
                {
                    password += keyPressed.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (keyPressed.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password.Substring(0, (password.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }

            while (keyPressed.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }

        // Method to receive ESC, CTRL Q or Enter
        public Enums.ExitOptions InputExitChoice()
            
        {
            do
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                if (keyPressed.Key == ConsoleKey.Escape)
                {
                    return Enums.ExitOptions.Esc;
                }
                else if (keyPressed.Modifiers == ConsoleModifiers.Control & keyPressed.Key == ConsoleKey.Q)
                {
                    return Enums.ExitOptions.CtrlQ;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    return Enums.ExitOptions.Enter;
                }
            } while (true);
        }







    }
} 

