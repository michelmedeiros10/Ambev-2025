using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
	public class ListSaleProfile : Profile
	{
		public ListSaleProfile()
		{
			CreateMap<ListSaleRequest, ListSaleCommand>();
		}
	}
}
