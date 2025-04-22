using Ambev.DeveloperEvaluation.Common.Service;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
	public class SaleDeletedEvent : IEvent, IIntegrationEvent
	{
		public Guid Oid { get; set; }
		public Guid Sid { get; set; }
		public Guid Id { get; init; } = Guid.NewGuid();
		public string Type { get; set; } = string.Empty;

		public string SaleId { get; }

		public SaleDeletedEvent(string saleId)
		{
			SaleId = saleId;
			Type = GetType().FullName;
		}
	}
}
