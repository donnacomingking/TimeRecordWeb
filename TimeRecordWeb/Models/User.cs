using System.ComponentModel.DataAnnotations;

namespace TimeRecordWeb.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Token { get; set; }
        public int TokenExpiresIn { get; set; }

        public User()
        {
            UserName = "TestUser";
            Password = "Smoothies01!";
        }
    }
}