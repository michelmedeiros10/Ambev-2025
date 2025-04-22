namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Represents the response returned after successfully updating a new product.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the updated product,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateProductResult
{
	/// <summary>
	/// Gets or sets the unique identifier of the updated product.
	/// </summary>
	/// <value>A GUID that uniquely identifies the updated product in the system.</value>
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
