using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Command for updating a new user.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for updating a user, 
/// including username, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="UpdateUserResult"/>.
/// 
/// The data provided in this command is validated using the 
/// populated and follow the required rules.
/// </remarks>
public class UpdateUserCommand : IRequest<UpdateUserResult>
{
    /// <summary>
    /// User Id
    /// </summary>
    public string Id { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the username of the user to be created.
	/// </summary>
	public string Username { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the password for the user.
	/// </summary>
	public string Password { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the phone number for the user.
	/// </summary>
	public string Phone { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the email address for the user.
	/// </summary>
	public string Email { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the status of the user.
	/// </summary>
	public UserStatus Status { get; set; }

	/// <summary>
	/// Gets or sets the role of the user.
	/// </summary>
	public UserRole Role { get; set; }

	/// <summary>
	/// First name and last name
	/// </summary>
	public PersonNameCommand? Name { get; set; }

	/// <summary>
	/// Address
	/// </summary>
	public PersonAddressCommand? Address { get; set; }

}

