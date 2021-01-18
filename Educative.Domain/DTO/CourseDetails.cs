using System.Collections.Generic;

namespace Educative.Domain.DTO
{
    public class CourseDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public MediaObjectDetails image { get; set; }
        public TrackCollection track { get; set; }
        public List<TutorialCollection> tutorials { get; set; }
    }
}
