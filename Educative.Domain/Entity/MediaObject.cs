using System.ComponentModel.DataAnnotations;

namespace Educative.Domain.Entity
{
    public class MediaObject
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string path { get; set; }
    }
}
