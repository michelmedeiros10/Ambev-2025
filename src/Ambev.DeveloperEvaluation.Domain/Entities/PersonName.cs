using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
	public class PersonName : BaseEntity
	{
		/// <summary>
		/// First name for user
		/// </summary>
		public string FirstName { get; set; } = string.Empty;
		/// <summary>
		/// Last name for user
		/// </summary>
		public string LastName { get; set; } = string.Empty;
        /// <summary>
		/// User Id reference
		/// </summary>
		public Guid? UserId { get; set; }
        /// <summary>
		/// User object
		/// </summary>
		public User? User { get; set; }
    }
}
