using System.ComponentModel.DataAnnotations;

namespace Lap_Shop.Models
{
    public class LogInUser
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]

        public string Password { get; set; } = string.Empty;

    }
}
