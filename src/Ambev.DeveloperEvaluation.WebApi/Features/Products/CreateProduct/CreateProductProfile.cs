using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Profile for mapping between Application and API CreateProduct responses
/// </summary>
public class CreateProductProfile : Profile
{
	/// <summary>
	/// Initializes the mappings for CreateProduct feature
	/// </summary>
	public CreateProductProfile()
	{
		CreateMap<CreateProductRequest, CreateProductCommand>()
			.ForPath(dest => dest.Rate, opt => opt.MapFrom(src => src.Rating.Rate))
			.ForPath(dest => dest.Count, opt => opt.MapFrom(src => src.Rating.Count))
			;

		CreateMap<CreateProductResult, CreateProductResponse>();
	}
}
