using AutoMapper;
using CleanArchitecture.Application.Features.ApplicationUser.Commands.Requests;
using CleanArchitecture.Application.Features.ApplicationUser.Queries.Response;
using CleanArchitecture.Domain.Entities.Identity;

namespace CleanArchitecture.Application.Mapping
{
    public class ApplicationUser : Profile
    {
        public ApplicationUser()
        {
            CreateMap<AddUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            CreateMap<User, GetUserReponseQuery>();
        }
    }
}
