namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSales
{
	public class ListSaleRequest
	{
		public string SortOrder { get; set; } = string.Empty;
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
	}
}
