namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// API response model for GetProduct operation
/// </summary>
public class GetProductResponse
{
	/// <summary>
	/// The unique identifier of the product
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Product title
	/// </summary>
	public string Title { get; set; } = string.Empty;
	/// <summary>
	/// Product price
	/// </summary>
	public decimal Price { get; set; }
	/// <summary>
	/// Product description
	/// </summary>
	public string Description { get; set; } = string.Empty;
	/// <summary>
	/// Product Category
	/// </summary>
	public string Category { get; set; } = string.Empty;
	/// <summary>
	/// Product image
	/// </summary>
	public string Image { get; set; } = string.Empty;
	/// <summary>
	/// Product rate
	/// </summary>
	public decimal Rate { get; set; }
	/// <summary>
	/// Product count
	/// </summary>
	public int Count { get; set; }
}
