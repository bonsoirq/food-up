using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodUp.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public string EncryptedPassword { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }


        public void EncryptPassword() {
            EncryptedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            Password = null;
        }
    }
}
