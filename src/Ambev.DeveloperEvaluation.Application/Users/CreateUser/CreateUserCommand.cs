using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Command for creating a new user.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a user, 
/// including username, password, phone number, email, status, and role. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateUserResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateUserCommandValidator"/> which extends 
/// populated and follow the required rules.
/// </remarks>
public class CreateUserCommand : IRequest<CreateUserResult>
{
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

public class PersonNameCommand
{
	/// <summary>
	/// First name
	/// </summary>
	public string FirstName { get; set; } = string.Empty;
	/// <summary>
	/// Last name
	/// </summary>
	public string LastName { get; set; } = string.Empty;
}

public class PersonAddressCommand
{
	/// <summary>
	/// City
	/// </summary>
	public string City { get; set; } = string.Empty;
	/// <summary>
	/// Street
	/// </summary>
	public string Street { get; set; } = string.Empty;
	/// <summary>
	/// Number
	/// </summary>
	public int Number { get; set; }
	/// <summary>
	/// Zipcode
	/// </summary>
	public string Zipcode { get; set; } = string.Empty;
	/// <summary>
	/// Geolocation Latitude
	/// </summary>
	public string GeoLatitude { get; set; } = string.Empty;
	/// <summary>
	/// Geolocation Latitude
	/// </summary>
	public string GeoLongitude { get; set; } = string.Empty;
}