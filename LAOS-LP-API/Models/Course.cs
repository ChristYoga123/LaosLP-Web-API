using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAOS_LP_API.Models
{
    public class Course
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        [ForeignKey("Category")]
        public int category_id { get; set; }
        public virtual Category? Category { get; set; }
        public int price { get; set; }
    }
}
