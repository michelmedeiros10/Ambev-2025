﻿using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Profile for mapping between Sale entity and GetSaleResponse
/// </summary>
public class GetSaleProfile : Profile
{
	/// <summary>
	/// Initializes the mappings for GetSale operation
	/// </summary>
	public GetSaleProfile()
	{
		CreateMap<SaleProduct, SaleProductResult>()
			.ForPath(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id));
		CreateMap<Sale, GetSaleResult>();
	}
}
