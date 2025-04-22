using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale in the system.
/// </summary>
public class CreateSaleRequest
{
	/// <summary>
	/// Sale number
	/// </summary>
	public int SaleNumber { get; set; }
	/// <summary>
	/// Sale date
	/// </summary>
	public DateTime SaleDate { get; set; }
	/// <summary>
	/// Sale customer Id
	/// </summary>
	public Guid? CustomerId { get; set; }
	/// <summary>
	/// Total sale amount
	/// </summary>
	public decimal Amount { get; set; }
	/// <summary>
	/// Branch where the sale was made
	/// </summary>
	public string Branch { get; set; } = string.Empty;
	/// <summary>
	/// Sales products
	/// </summary>
	public List<SaleProductRequest> Products { get; set; } = new List<SaleProductRequest>();
}

public class SaleProductRequest
{
	/// <summary>
	/// Product Id
	/// </summary>
	public string ProductId { get; set; } = string.Empty;
	/// <summary>
	/// Product unit price
	/// </summary>
	public decimal Price { get; set; }
    /// <summary>
	/// Sale quantity of the product
	/// </summary>
	public decimal Quantity { get; set; }
    /// <summary>
    /// Product discount
    /// </summary>
    public decimal Discount { get; set; }
	/// <summary>
	/// Total amount for this product
	/// </summary>
	public decimal TotalAmount { get; set; }

}