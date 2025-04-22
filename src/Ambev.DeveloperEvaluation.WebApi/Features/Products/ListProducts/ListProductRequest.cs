namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts
{
	public class ListProductRequest
	{
		public string SortOrder { get; set; } = string.Empty;
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
	}
}
