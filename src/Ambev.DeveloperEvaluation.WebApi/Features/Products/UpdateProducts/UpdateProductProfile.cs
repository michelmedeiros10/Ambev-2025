using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Application and API UpdateProduct responses
/// </summary>
public class UpdateProductProfile : Profile
{
	/// <summary>
	/// Initializes the mappings for UpdateProduct feature
	/// </summary>
	public UpdateProductProfile()
	{
		CreateMap<UpdateProductRequest, UpdateProductCommand>()
			.ForPath(dest => dest.Rate, opt => opt.MapFrom(src => src.Rating.Rate))
			.ForPath(dest => dest.Count, opt => opt.MapFrom(src => src.Rating.Count))
			;

		CreateMap<UpdateProductResult, UpdateProductResponse>();
	}
}
