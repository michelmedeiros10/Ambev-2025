using Ambev.DeveloperEvaluation.Common.Service;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Users
{
	public class UserDeletedEvent : IEvent, IIntegrationEvent
	{
		public Guid Oid { get; set; }
		public Guid Sid { get; set; }
		public Guid Id { get; init; } = Guid.NewGuid();
		public string Type { get; set; } = string.Empty;

		public string UserId { get; }

		public UserDeletedEvent(string userId)
		{
			UserId = userId;
			Type = GetType().FullName;
		}
	}
}
