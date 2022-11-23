using System.ComponentModel.DataAnnotations;

namespace LAOS_LP_API.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
