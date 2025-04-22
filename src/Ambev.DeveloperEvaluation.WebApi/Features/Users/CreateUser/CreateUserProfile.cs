using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateUserProfile()
    {
		CreateMap<PersonNameRequest, PersonNameCommand>();
		CreateMap<PersonAddressRequest, PersonAddressCommand>();

		CreateMap<CreateUserRequest, CreateUserCommand>()
			.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

		CreateMap<CreateUserResult, CreateUserResponse>();
    }
}
