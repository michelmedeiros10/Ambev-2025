using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Response model for GetUser operation
/// </summary>
public class GetUserResult
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

public class PersonNameResult
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

public class PersonAddressResult
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