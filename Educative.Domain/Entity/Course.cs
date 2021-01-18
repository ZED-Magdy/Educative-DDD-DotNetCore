using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Educative.Domain.Entity
{
    public class Course
    {
        [Key]
        public int id { get; set; }
        [Required]
        [MaxLength(255)]
        [MinLength(30)]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public Track track { get; set; }

        public List<Tutorial> tutorials { get; set; }
        public MediaObject image { get; set; }
    }
}
