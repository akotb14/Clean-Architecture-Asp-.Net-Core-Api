namespace CleanArchitecture.Application.ResultHandler
{
    public class UserClaimsResult
    {
        public string UserId { get; set; }
        public List<UserClaims> UserClaims { get; set; }
    }
    public class UserClaims
    {
        public string ClaimName { get; set; }
        public bool HasClaim { get; set; }
    }
}
