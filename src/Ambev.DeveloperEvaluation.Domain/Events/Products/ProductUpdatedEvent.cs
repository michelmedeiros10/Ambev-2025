using Ambev.DeveloperEvaluation.Common.Service;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Products
{
	public class ProductUpdatedEvent : IEvent, IIntegrationEvent
	{
		public Guid Oid { get; set; }
		public Guid Sid { get; set; }
		public Guid Id { get; init; } = Guid.NewGuid();
		public string Type { get; set; } = string.Empty;

		public Product Product { get; }

		public ProductUpdatedEvent(Product user)
		{
			Product = user;
			Type = GetType().FullName;
		}
	}
}
