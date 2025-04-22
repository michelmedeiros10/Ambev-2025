using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents a request to update a product in the system.
/// </summary>
public class UpdateProductRequest
{
	/// <summary>
	/// Product Id
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
	/// Product Rating
	/// </summary>
	public ProductRating Rating { get; set; } = new ProductRating();

}
