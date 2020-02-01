using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodUp.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Login { get; set; }
        [NotMapped]
        [Required]
        [MinLength(8)]
        [MaxLength(32)]
        public string Password { get; set; }
        public string EncryptedPassword { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }


        public void EncryptPassword() {
            EncryptedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
            Password = null;
        }
    }
}
