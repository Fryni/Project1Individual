
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Project1Individual
{
    public class Message
    {   
        [Key]
        public int MessageId { get; set; }

        
        public DateTime DateCreated { get; set; }

        [MaxLength(250), MinLength(1)]
        
        public string Content { get; set; }

        [MaxLength(30), MinLength(0)]
     
        public string Subject { get; set; }

        public bool IsMessageActive { get; set; }

        
        public User Sender { get; set; }

        
        public User Recipient { get; set; }  
    }



}

