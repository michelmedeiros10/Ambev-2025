using Ambev.DeveloperEvaluation.Application.Users.ListUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
	public class ListUserProfile : Profile
	{
        public ListUserProfile()
        {
			CreateMap<ListUserRequest, ListUserCommand>();
		}
	}
}
