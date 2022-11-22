using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LAOS_LP_API.Models
{
    public class Lesson
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        [ForeignKey("Course")]
        public int course_id { get; set; }
        public Course? Course { get; set; }
    }
}
