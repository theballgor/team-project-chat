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
        DoNotDisturb,
        None
    }
    [Table("Users")]
    [Serializable]
    class User
    {
        [Key]
        public int Id;
        public string Username;
        public string Email;
        public string Password;
        public string Avatar;
        // public UserStatus UserStatus;
    }
}