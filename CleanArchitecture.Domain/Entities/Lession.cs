namespace CleanArchitecture.Domain.Entities
{
    public class Lession
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
