using System.ComponentModel.DataAnnotations;

namespace Educative.Domain.Entity
{
    public class Tutorial
    {
        [Key]
        public int id { get; set; }

        [Required]
        [MaxLength(255)]
        [MinLength(30)]
        public string name { get; set; }

        [Required]
        [MinLength(50)]
        public string content { get; set; }

        [Required]
        public Course course { get; set; }
        public MediaObject image { get; set; }

    }
}
