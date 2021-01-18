using System.Collections.Generic;

namespace Educative.Domain.DTO
{
    public class TrackDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public MediaObjectDetails image { get; set; }
        public List<CourseCollection> courses { get; set; }
    }
}
