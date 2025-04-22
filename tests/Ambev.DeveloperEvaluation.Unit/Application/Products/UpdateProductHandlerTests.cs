using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="UpdateProductHandler"/> class.
/// </summary>
public class UpdateProductHandlerTests
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;
	private readonly UpdateProductHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="UpdateProductHandlerTests"/> class.
	/// Sets up the test dependencies and updates fake data generators.
	/// </summary>
	public UpdateProductHandlerTests()
	{
		_productRepository = Substitute.For<IProductRepository>();
		_mapper = Substitute.For<IMapper>();
		_eventBus = Substitute.For<IEventBus>();
		_handler = new UpdateProductHandler(
			_productRepository,
			_mapper, 
			_eventBus);
	}

	/// <summary>
	/// Tests that a valid product creation request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = UpdateProductHandlerTestData.GenerateValidCommand();
		var product = new Product
		{
			Id = Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710"),
			Category = command.Category,
			Count = command.Count,
			Description = command.Description,
			Image = command.Image,
			Price = command.Price,
			Rate = command.Rate,
			Title = command.Title
		};

		var result = new UpdateProductResult
		{
			Id = product.Id,
		};

		_mapper.Map<Product>(command).Returns(product);
		_mapper.Map<UpdateProductResult>(product).Returns(result);

		_productRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(product);
		_productRepository.Update(Arg.Any<Product>())
			.Returns(product);

		// When
		var updateProductResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		updateProductResult.Should().NotBeNull();
		updateProductResult.Id.Should().Be(product.Id);
		_productRepository.Received(1).Update(Arg.Any<Product>());
	}

	/// <summary>
	/// Tests that an invalid product creation request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
	public async Task Handle_InvalidRequest_ThrowsValidationException()
	{
		// Given
		var command = new UpdateProductCommand { Id = "218d4588-be6d-4fe9-8278-bf3a43914710" }; // Empty command will fail validation

		_productRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(default(Product));

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<InvalidOperationException>();
	}

	/// <summary>
	/// Tests that an invalid product Id request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid product Id data When updating product Then throws validation exception")]
	public async Task Handle_InvalidId_ThrowsValidationException()
	{
		// Given
		var command = new UpdateProductCommand { Id = "218d4588-be6d-4fe9-8278-bf3a43914710" }; // Empty command will fail validation

		_productRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(new Product());

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}

	/// <summary>
	/// Tests that the mapper is called with the correct command.
	/// </summary>
	[Fact(DisplayName = "Given valid command When handling Then maps command to product entity")]
	public async Task Handle_ValidRequest_MapsCommandToProduct()
	{
		// Given
		var command = UpdateProductHandlerTestData.GenerateValidCommand();
		var product = new Product
		{
			Id = Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710"),
			Category = command.Category,
			Count = command.Count,
			Description = command.Description,
			Image = command.Image,
			Price = command.Price,
			Rate = command.Rate,
			Title = command.Title
		};

		_mapper.Map<Product>(command).Returns(product);
		_productRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(product);
		_productRepository.Update(Arg.Any<Product>())
			.Returns(product);

		// When
		await _handler.Handle(command, CancellationToken.None);

		// Then
		_mapper.Received(1).Map<Product>(Arg.Is<UpdateProductCommand>(c =>
			c.Category == command.Category &&
			c.Count == command.Count &&
			c.Description == command.Description &&
			c.Image == command.Image &&
			c.Price == command.Price &&
			c.Rate == command.Rate &&
			c.Title == command.Title));
	}
}
