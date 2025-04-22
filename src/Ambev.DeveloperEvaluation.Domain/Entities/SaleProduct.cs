using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
	public class SaleProduct : BaseEntity
	{
		/// <summary>
		/// Product Id
		/// </summary>
		[NotMapped]
		public string ProductRefId { get; set; } = string.Empty;
		/// <summary>
		/// Product reference
		/// </summary>
		public Product? Product { get; set; }
  //      /// <summary>
		///// Sale Id
		///// </summary>
		//public string SaleId { get; set; } = string.Empty;
        /// <summary>
		/// Sale reference
		/// </summary>
		public Sale? Sale { get; set; }
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
}
