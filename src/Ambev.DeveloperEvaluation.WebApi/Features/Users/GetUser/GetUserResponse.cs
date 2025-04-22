using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// API response model for GetUser operation
/// </summary>
public class GetUserResponse
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
	/// The user's full name
	/// </summary>
	public GetPersonNameResponse? Name { get; set; }

	/// <summary>
	/// Address
	/// </summary>
	public GetPersonAddressResponse? Address { get; set; }

}

public class GetPersonNameResponse
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

public class GetPersonAddressResponse
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
	/// Geolocation
	/// </summary>
	public GetPersonAddressGeolocation? Geolocation { get; set; } = new GetPersonAddressGeolocation();
}

public class GetPersonAddressGeolocation
{
    /// <summary>
    /// Geolocation Latitude
    /// </summary>
    public string Lat { get; set; } = string.Empty;

	/// <summary>
	/// Geolocation Longitude
	/// </summary>
	public string Long { get; set; } = string.Empty;

}
