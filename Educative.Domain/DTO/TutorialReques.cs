namespace Educative.Domain.DTO
{
    public class TutorialRequest
    {
        public string name { get; set; }
        public string content { get; set; }
        public int? courseId { get; set; }
        public int? imageId { get; set; }
    }
}
