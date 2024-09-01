﻿namespace CleanArchitecture.Domain.Entities.Identity
{
    public class UserRefreshToken
    {

        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual User? user { get; set; }
    }
}
