namespace CleanArchitecture.Domain.Entities
{
    public class Course
    {
        public Course()
        {
            Lessions = new HashSet<Lession>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Lession> Lessions { get; set; }
    }
}
