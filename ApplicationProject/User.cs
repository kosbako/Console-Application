using ApplicationProject.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationProject
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        [MinLength(4, ErrorMessage = "The UserName should be 4 characters or more")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PassWord is required")]
        [MinLength(4, ErrorMessage = "The PassWord should be 4 characters or more")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "The Role is required")]
        public Role Privilege { get; set; }

        [InverseProperty("Sender")]
        public virtual ICollection<Message> MessagesSended { get; set; }

        [InverseProperty("Receiver")]
        public virtual ICollection<Message> MessagesReceived { get; set; }
    }
}
