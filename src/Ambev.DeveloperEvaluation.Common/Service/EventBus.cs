using HiveMQtt.Client;
using HiveMQtt.MQTT5.Types;
using System.Runtime.CompilerServices;
using System.Text.Json;

public sealed class EventBus(InMemoryMessageQueue queue) : IEventBus
{
	public async Task PublishAsync<T>(
		T integrationEvent,
		CancellationToken cancellationToken = default)
		where T : class, IIntegrationEvent
	{
		//await queue.Writer.WriteAsync(integrationEvent, cancellationToken);

		try
		{
			var options = new HiveMQClientOptionsBuilder()
						.WithBroker("8b7478c7cd2e4193827e89f0aa0e45d2.s1.eu.hivemq.cloud")
						.WithPort(8883)
						.WithUseTls(true)
						.WithUserName("michelmedeiros10")
						.WithPassword("4Mb3v@2025")
						.Build();

			var client = new HiveMQClient(options);
			var connectResult = await client.ConnectAsync().ConfigureAwait(false);

			var serialized = JsonSerializer.Serialize(integrationEvent);

			var publishResult = await client.PublishAsync("ambev", serialized, QualityOfService.AtMostOnceDelivery);
		}
		catch (Exception ex)
		{
		}
	}
}