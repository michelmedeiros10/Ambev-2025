using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="DeleteProductHandler"/> class.
/// </summary>
public class DeleteProductHandlerTests
{
	private readonly IProductRepository _productRepository;
	private readonly IEventBus _eventBus;
	private readonly DeleteProductHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="DeleteProductHandlerTests"/> class.
	/// Sets up the test dependencies and deletes fake data generators.
	/// </summary>
	public DeleteProductHandlerTests()
	{
		_productRepository = Substitute.For<IProductRepository>();
		_eventBus = Substitute.For<IEventBus>();

		_handler = new DeleteProductHandler(
			_productRepository,
			_eventBus);
	}

	/// <summary>
	/// Tests that a valid product deleting request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid product data When deleting product Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = DeleteProductHandlerTestData.GenerateValidCommand();

		_productRepository.DeleteAsync(Arg.Any<Guid>())
			.Returns(true);

		// When
		var deleteProductResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		deleteProductResult.Should().NotBeNull();
		await _productRepository.Received(1).DeleteAsync(Arg.Any<Guid>());
	}

	/// <summary>
	/// Tests that an invalid product deleting request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid product data When deleting product Then throws validation exception")]
	public async Task Handle_InvalidRequest_ThrowsValidationException()
	{
		// Given
		var command = new DeleteProductCommand(Guid.Empty); // Empty command will fail validation

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}
}
