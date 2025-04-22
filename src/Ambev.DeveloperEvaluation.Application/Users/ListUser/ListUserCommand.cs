using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUser;

/// <summary>
/// Command for retrieving a list of users
/// </summary>
public record ListUserCommand : IRequest<ListUserResult>
{
	public string SortOrder { get; set; } = string.Empty;
	public int PageSize { get; set; }
	public int PageNumber { get; set; }

}
