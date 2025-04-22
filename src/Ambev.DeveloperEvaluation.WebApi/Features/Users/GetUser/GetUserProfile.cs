using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
		CreateMap<PersonNameResult, GetPersonNameResponse>();
		CreateMap<PersonAddressResult, GetPersonAddressResponse>();

		CreateMap<PersonAddressResult, GetPersonAddressResponse>()
			.ForPath(dest => dest.Geolocation.Lat, opt => opt.MapFrom(src => src.GeoLatitude))
			.ForPath(dest => dest.Geolocation.Long, opt => opt.MapFrom(src => src.GeoLongitude));

		CreateMap<Guid, GetUserCommand>()
            .ConstructUsing(id => new GetUserCommand(id));

		CreateMap<GetUserResult, GetUserResponse>()
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
	}
}
