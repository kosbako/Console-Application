using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationProject.Controllers;

namespace ApplicationProject
{
    public class Message
    {
        public Message()
        {
            this.DateOfSubmission = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        
        public DateTime DateOfSubmission { get; set; }

        [StringLength (250, ErrorMessage = "The messagedata should be up to 250 characters")]
        [MinLength(1, ErrorMessage = "The Message can't be blank")]
        public string MessageData { get; set; }

        //Navigation property
        [ForeignKey("Sender")] 
        public int? SenderId { get; set; } 
        public virtual User Sender { get; set; }

        //Navigation property
        [ForeignKey("Receiver")] 
        public int? ReceiverId { get; set; } 
        public virtual User Receiver { get; set; }    
    }
}
