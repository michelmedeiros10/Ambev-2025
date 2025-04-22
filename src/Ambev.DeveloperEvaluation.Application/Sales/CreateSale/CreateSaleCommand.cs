using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a sale, 
/// including salename, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateSaleResult"/>.
/// 
/// The data provided in this command is validated using the 
/// populated and follow the required rules.
/// </remarks>
public class CreateSaleCommand : IRequest<CreateSaleResult>
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
	public List<SaleProductCommand> Products { get; set; } = new List<SaleProductCommand>();
}

public class SaleProductCommand
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