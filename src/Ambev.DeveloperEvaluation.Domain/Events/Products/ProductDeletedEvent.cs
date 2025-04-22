using Ambev.DeveloperEvaluation.Common.Service;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Products
{
	public class ProductDeletedEvent : IEvent, IIntegrationEvent
	{
		public Guid Oid { get; set; }
		public Guid Sid { get; set; }
		public Guid Id { get; init; } = Guid.NewGuid();
		public string Type { get; set; } = string.Empty;

		public string ProductId { get; }

		public ProductDeletedEvent(string id)
		{
			ProductId = id;
			Type = GetType().FullName;
		}
	}
}
