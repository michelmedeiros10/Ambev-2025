using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Command for retrieving a list of products
/// </summary>
public record ListProductCommand : IRequest<ListProductResult>
{
	public string SortOrder { get; set; } = string.Empty;
	public int PageSize { get; set; }
	public int PageNumber { get; set; }

}
