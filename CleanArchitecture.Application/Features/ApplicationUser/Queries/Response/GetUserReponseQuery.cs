﻿namespace CleanArchitecture.Application.Features.ApplicationUser.Queries.Response
{
    public class GetUserReponseQuery
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageProfile { get; set; }
    }
}
