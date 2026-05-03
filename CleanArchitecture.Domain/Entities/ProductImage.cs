namespace CleanArchitecture.Domain.Entities
{
    public class ProductImage
    {
        public int ImageID { get; set; }
        public int ProductID { get; set; }
        public string ImageURL { get; set; }
        public string? AltText { get; set; }
        public int Order { get; set; }

        public Product Product { get; set; }
    }

}