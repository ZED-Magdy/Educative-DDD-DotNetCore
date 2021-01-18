namespace Educative.Domain.DTO
{
    public class TutorialDetails
    {
        public int id { get; set; }
        public string name { get; set; }
        public string content { get; set; }
        public MediaObjectDetails image { get; set; }
        public CourseCollection course { get; set; }
    }
}
