using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="ListProductHandler"/> class.
/// </summary>
public class ListProductHandlerTests
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;
	private readonly ListProductHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="ListProductHandlerTests"/> class.
	/// Sets up the test dependencies and lists fake data generators.
	/// </summary>
	public ListProductHandlerTests()
	{
		_productRepository = Substitute.For<IProductRepository>();

		var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ListProductResult>());

		_mapper = config.CreateMapper();

		_handler = new ListProductHandler(
			_productRepository,
			_mapper);
	}

	/// <summary>
	/// Tests that a valid product listing request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid product data When listing product Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = ListProductHandlerTestData.GenerateValidCommand();
		var products = ListProductHandlerTestData.GenerateValidProductsList();

		_productRepository.GetAll(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>())
			.Returns(products);

		// When
		var listProductResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		await _productRepository.Received(1).GetAll(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());

		listProductResult.Should().NotBeNull();
		listProductResult.Products.Should().NotBeNull();
		listProductResult.Products.Count.Should().Be(products.Count);
		listProductResult.Products.First().Should().BeEquivalentTo(products.First());
		listProductResult.Products.Last().Should().BeEquivalentTo(products.Last());
	}

	/// <summary>
	/// Tests that an empty list of products throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Empty list When listing product Then throws KeyNotFoundException exception")]
	public async Task Handle_EmptyList_ThrowsValidationException()
	{
		// Given
		var command = new ListProductCommand(); // Empty command will fail validation

		_productRepository.GetAll()
			.Returns(new List<Product>());

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<KeyNotFoundException>();
	}
}
