namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers
{
	public class ListUserRequest
	{
        public string SortOrder { get; set; } = string.Empty;
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
