using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project1Individual
{
    
        public class User
        {
            [Key]
            public int UserId { get; set; } 

            [Required]
            public string Username { get; set; }

            [Required]  
            public string Password { get; set; }

            [Required]
            public DateTime DateCreated { get; set; }

            [Required]
            public int UserType { get; set; }

            public bool IsUserActive { get; set; } // to check for deleted users

        

            [InverseProperty("Sender")]
            public ICollection<Message> SentMessages { get; set; } 

            [InverseProperty("Recipient")]
            public ICollection<Message> ReceivedMessages { get; set; } 

            //constructor
            public User(string username, string password, int userType)
            {
                Username = username;
                Password = password;
                DateCreated = DateTime.Now;
                UserType = userType;
                IsUserActive = true;

            }

            // default constructor
            public User()
            {

            }

        }
 }

