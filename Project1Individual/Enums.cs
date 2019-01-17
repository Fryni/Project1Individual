using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1Individual
{
    public class Enums
    { 
        public enum MainMenuOptions
        {
            Login = 1,
            SignUp = 2,
            Info = 3,
            Exit = 4

        }

        public enum ExitOptions
        {

            Esc = 1,
            CtrlQ = 2,
            Enter = 3

        }

        public enum UserTypes
        {
            User = 1,
            JuniorAdmin = 2,
            MasterAdmin = 3,
            SuperAdmin = 4,
        }

        public enum UserMenuOptions
        {
            CreateNewMessage = 1,
            Inbox = 2,
            SentMessages = 3,
            Info = 4,
            ExitToMain = 5,
            Quit = 6
        }

        public enum JuniorAdminMenuOptions
        {
            CreateNewMessage = 1,
            Inbox = 2,
            SentMessages = 3,
            Info = 4,

            ViewUserInfo = 5,
            ViewUserMessages = 6,
            ViewAllMessages = 7,
            EditMessages = 8,

            ExitToMain = 9,
            Quit = 10
        }

        public enum MasterAdminMenuOptions
        {
            CreateNewMessage = 1,
            Inbox = 2,
            SentMessages = 3,
            Info = 4,

            ViewUserInfo = 5,
            ViewUserMessages = 6,
            ViewAllMessages = 7,
            EditMessages = 8,
            DeleteMessages = 9,

            ExitToMain = 10,
            Quit = 11
        }

        public enum SuperAdminMenuOptions
        {
            CreateNewMessage = 1,
            Inbox = 2,
            SentMessages = 3,
            Info = 4,

            CreateNewUser = 5,
            DeleteUser = 6,
            ActivateUser = 7,
            EditUserType = 8,
            ViewUserInfo = 9,

            ViewUserMessages = 10,
            ViewAllMessages = 11,
            DeleteMessages = 12,
            EditMessages = 13,

            ExitToMain = 14,
            Quit = 15
        }







    }
}
