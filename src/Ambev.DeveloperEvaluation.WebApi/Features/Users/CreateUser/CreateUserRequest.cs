using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Represents a request to create a new user in the system.
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Gets or sets the username. Must be unique and contain only valid characters.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password. Must meet security requirements.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number in format (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address. Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the initial status of the user account.
    /// </summary>
    public UserStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the role assigned to the user.
    /// </summary>
    public UserRole Role { get; set; }
	/// <summary>
	/// First name and last name
	/// </summary>
	public PersonNameRequest? Name { get; set; }
	/// <summary>
	/// Address
	/// </summary>
	public PersonAddressRequest? Address { get; set; }


}

public class PersonNameRequest
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

public class PersonAddressRequest
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