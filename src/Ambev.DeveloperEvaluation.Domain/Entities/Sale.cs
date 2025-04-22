using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
	public class Sale : BaseEntity
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
		/// Sale customer
		/// </summary>
		public Customer? Customer { get; set; }
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
		public List<SaleProduct>? Products { get; set; } = new List<SaleProduct>();
    }
}
