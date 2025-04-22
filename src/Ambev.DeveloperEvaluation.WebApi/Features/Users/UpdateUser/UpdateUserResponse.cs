using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
	public class UpdateUserResponse
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
}
