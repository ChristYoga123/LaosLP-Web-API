using System.ComponentModel.DataAnnotations;

namespace LAOS_LP_API.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
    }
}
