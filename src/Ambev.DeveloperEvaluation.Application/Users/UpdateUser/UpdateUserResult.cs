using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Represents the response returned after successfully updating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the updated user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateUserResult
{
	/// <summary>
	/// The unique identifier of the user
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// The user's email address
	/// </summary>
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// The user's phone number
	/// </summary>
	public string Phone { get; set; } = string.Empty;

	/// <summary>
	/// The user's role in the system
	/// </summary>
	public UserRole Role { get; set; }

	/// <summary>
	/// The current status of the user
	/// </summary>
	public UserStatus Status { get; set; }

	/// <summary>
	/// First name and last name
	/// </summary>
	public PersonNameResult? Name { get; set; }

	/// <summary>
	/// Address
	/// </summary>
	public PersonAddressResult? Address { get; set; }

}
