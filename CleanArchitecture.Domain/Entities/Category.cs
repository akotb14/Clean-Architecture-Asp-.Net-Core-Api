namespace CleanArchitecture.Domain.Entities
{
    public class Category
    {

        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryID { get; set; }
        public string Description { get; set; }

        public Category ParentCategory { get; set; }
        public ICollection<Category> SubCategories { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Discount> Discounts { get; set; }

    }
}
