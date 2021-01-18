

namespace Educative.Domain.DTO
{
    public class CourseRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public int? trackId { get; set; }
        public int? imageId { get; set; }
    }
}
