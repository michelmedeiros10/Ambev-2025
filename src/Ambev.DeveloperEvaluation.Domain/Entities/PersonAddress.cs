using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
	public class PersonAddress : BaseEntity
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
