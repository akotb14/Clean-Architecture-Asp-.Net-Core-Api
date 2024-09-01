namespace CleanArchitecture.Application.ResultHandler
{
    public class UserRolesResult
    {
        public string UserId { get; set; }
        public List<RoleResults> RoleResults { get; set; }
    }
    public class RoleResults
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasRole { get; set; }
    }
}
