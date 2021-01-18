using System.Collections.Generic;
namespace Educative.Domain.DTO
{
    public class TrackRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public int? imageId { get; set; }
    }
}
