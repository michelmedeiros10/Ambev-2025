using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between Product entity and UpdateProductResponse
/// </summary>
public class UpdateProductProfile : Profile
{
	/// <summary>
	/// Initializes the mappings for UpdateProduct operation
	/// </summary>
	public UpdateProductProfile()
	{
		CreateMap<UpdateProductCommand, Product>();
		CreateMap<Product, UpdateProductResult>();
		CreateMap<Product, Product>()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
			.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
			.ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
			.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
			.ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
			.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
			;
	}
}
