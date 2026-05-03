using CleanArchitecture.Domain.Entities.Identity;

namespace CleanArchitecture.Domain.Entities
{
    public class ProductReview
    {
        public int ReviewID { get; set; }
        public int ProductID { get; set; }
        public string UserID { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime CreatedDate { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}