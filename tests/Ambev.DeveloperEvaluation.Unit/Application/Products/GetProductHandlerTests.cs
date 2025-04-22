using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="GetProductHandler"/> class.
/// </summary>
public class GetProductHandlerTests
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;
	private readonly GetProductHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetProductHandlerTests"/> class.
	/// Sets up the test dependencies and gets fake data generators.
	/// </summary>
	public GetProductHandlerTests()
	{
		_productRepository = Substitute.For<IProductRepository>();

		var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, GetProductResult>());
		_mapper = config.CreateMapper();

		_handler = new GetProductHandler(
			_productRepository,
			_mapper);
	}

	/// <summary>
	/// Tests that a valid product getting request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid product data When getting product Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = GetProductHandlerTestData.GenerateValidCommand();
		var product = GetProductHandlerTestData.GenerateValidProduct();

		_productRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(product);

		// When
		var getProductResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		await _productRepository.Received(1).GetByIdAsync(Arg.Any<Guid>());

		getProductResult.Should().NotBeNull();
		getProductResult.Id.Should().Be(product.Id);
		getProductResult.Category.Should().Be(product.Category);
		getProductResult.Count.Should().Be(product.Count);
		getProductResult.Description.Should().Be(product.Description);
		getProductResult.Image.Should().Be(product.Image);
		getProductResult.Price.Should().Be(product.Price);
		getProductResult.Rate.Should().Be(product.Rate);
		getProductResult.Title.Should().Be(product.Title);
	}

	/// <summary>
	/// Tests that an invalid product deleting request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid product data When deleting product Then throws validation exception")]
	public async Task Handle_InvalidRequest_ThrowsValidationException()
	{
		// Given
		var command = new GetProductCommand(Guid.Empty); // Empty command will fail validation

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}

	/// <summary>
	/// Tests that an invalid product Id request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid product Id data When getting product Then throws validation exception")]
	public async Task Handle_InvalidId_ThrowsValidationException()
	{
		// Given
		var command = new GetProductCommand(Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710")); // Empty command will fail validation

		_productRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(default(Product));

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<KeyNotFoundException>();
	}
}
