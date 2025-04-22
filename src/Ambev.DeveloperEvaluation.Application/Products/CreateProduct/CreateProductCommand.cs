using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new product.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a product, 
/// including productname, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateProductResult"/>.
/// 
/// The data provided in this command is validated using the 
/// populated and follow the required rules.
/// </remarks>
public class CreateProductCommand : IRequest<CreateProductResult>
{
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