using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServerLibrary.DbClasses
{
    [Serializable]
    public enum UserStatus
    {
        Online,
        Ofline,
        DoNotDisturb
    }
    [Table("Users")]
    [Serializable]
    public class User
    {
        [Key]
        public int Id { get;}
        public string Username { get;private set; }
        public string Email { get; private set;}
        public string Password { get; private set; }
        public string Avatar { get; private set; }
        public string Description { get;  set; }
        public string PhoneNumber { get; set; }
        public UserStatus UserStatus { get; set; }
    }
}