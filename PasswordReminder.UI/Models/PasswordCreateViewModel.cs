using System.ComponentModel.DataAnnotations;

namespace PasswordReminder.UI.Models
{
    public class PasswordCreateViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string CategoryName { get; set; } // Seçilen ya da yeni girilen kategori
    }
}
