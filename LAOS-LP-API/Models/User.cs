using System.ComponentModel.DataAnnotations;

namespace LAOS_LP_API.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool is_admin { get; set; }
    }
}