using System.Security.Claims;

namespace CleanArchitecture.Domain.Helper
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create Course","false"),
            new Claim("Edit Course","false"),
            new Claim("Delete Course","false"),
            new Claim("Create User","false"),
            new Claim("Edit User","false"),
            new Claim("Delete User","false"),
        };
    }
}
