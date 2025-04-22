using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
	public class UpdateUserRequest
	{
		/// <summary>
		/// User Id
		/// </summary>
		public Guid Id { get; set; }

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
}
