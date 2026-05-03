namespace CleanArchitecture.Application.Features.Categories.Queries.Responses
{
    public class GetCategoryResponse
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int? ParentCategoryID { get; set; }
        public string Description { get; set; }
    }
}
