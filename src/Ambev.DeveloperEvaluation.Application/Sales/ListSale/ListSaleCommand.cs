using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale;

/// <summary>
/// Command for retrieving a list of sales
/// </summary>
public record ListSaleCommand : IRequest<ListSaleResult>
{
	public string SortOrder { get; set; } = string.Empty;
	public int PageSize { get; set; }
	public int PageNumber { get; set; }

}
