using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Profile for mapping between Sale entity and CreateSaleResponse
/// </summary>
public class CreateSaleProfile : Profile
{
	/// <summary>
	/// Initializes the mappings for CreateSale operation
	/// </summary>
	public CreateSaleProfile()
	{
		CreateMap<SaleProductCommand, SaleProduct>()
			.ForMember(dest => dest.ProductRefId, opt => opt.MapFrom(src => src.ProductId));
		CreateMap<CreateSaleCommand, Sale>()
			.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

		CreateMap<Sale, CreateSaleResult>();
	}
}
