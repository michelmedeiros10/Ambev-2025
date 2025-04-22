using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="DeleteSaleHandler"/> class.
/// </summary>
public class DeleteSaleHandlerTests
{
	private readonly ISaleRepository _saleRepository;
	private readonly IEventBus _eventBus;
	private readonly DeleteSaleHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="DeleteSaleHandlerTests"/> class.
	/// Sets up the test dependencies and deletes fake data generators.
	/// </summary>
	public DeleteSaleHandlerTests()
	{
		_saleRepository = Substitute.For<ISaleRepository>();
		_eventBus = Substitute.For<IEventBus>();

		_handler = new DeleteSaleHandler(
			_saleRepository,
			_eventBus);
	}

	/// <summary>
	/// Tests that a valid sale deleting request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid sale data When deleting sale Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = DeleteSaleHandlerTestData.GenerateValidCommand();

		_saleRepository.DeleteAsync(Arg.Any<Guid>())
			.Returns(true);

		// When
		var deleteSaleResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		deleteSaleResult.Should().NotBeNull();
		await _saleRepository.Received(1).DeleteAsync(Arg.Any<Guid>());
	}

	/// <summary>
	/// Tests that an invalid sale deleting request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid sale data When deleting sale Then throws validation exception")]
	public async Task Handle_InvalidRequest_ThrowsValidationException()
	{
		// Given
		var command = new DeleteSaleCommand(Guid.Empty); // Empty command will fail validation

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}
}
