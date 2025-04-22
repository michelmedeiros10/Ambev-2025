using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
		CreateMap<PersonNameCommand, PersonName>();
		CreateMap<PersonAddressCommand, PersonAddress>();

		CreateMap<CreateUserCommand, User>()
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<User, CreateUserResult>();
    }
}
