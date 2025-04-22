using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Profile for mapping between User entity and UpdateUserResponse
/// </summary>
public class UpdateUserProfile : Profile
{
	/// <summary>
	/// Initializes the mappings for UpdateUser operation
	/// </summary>
	public UpdateUserProfile()
	{
		CreateMap<UpdateUserCommand, User>()
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

		CreateMap<User, User>()
			.ForPath(dest => dest.Name.FirstName, opt => opt.MapFrom(src => src.Name.FirstName))
			.ForPath(dest => dest.Name.LastName, opt => opt.MapFrom(src => src.Name.LastName))
			.ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.Address.City))
			.ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Address.Street))
			.ForPath(dest => dest.Address.Number, opt => opt.MapFrom(src => src.Address.Number))
			.ForPath(dest => dest.Address.Zipcode, opt => opt.MapFrom(src => src.Address.Zipcode))
			.ForPath(dest => dest.Address.GeoLatitude, opt => opt.MapFrom(src => src.Address.GeoLatitude))
			.ForPath(dest => dest.Address.GeoLongitude, opt => opt.MapFrom(src => src.Address.GeoLongitude))
			;

		CreateMap<User, UpdateUserResult>();
	}
}
