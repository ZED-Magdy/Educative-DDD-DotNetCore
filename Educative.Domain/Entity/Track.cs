using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Educative.Domain.Entity
{
    public class Track
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(30)]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        public List<Course> courses { get; set; }
        public MediaObject image { get; set; }
    }
}
