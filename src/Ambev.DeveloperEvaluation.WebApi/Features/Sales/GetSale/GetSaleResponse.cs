namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for GetSale operation
/// </summary>
public class GetSaleResponse
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
	public List<SaleProductResponse> Products { get; set; } = new List<SaleProductResponse>();

}

public class SaleProductResponse
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