using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="ListSaleHandler"/> class.
/// </summary>
public class ListSaleHandlerTests
{
	private readonly ISaleRepository _saleRepository;
	private readonly IMapper _mapper;
	private readonly ListSaleHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="ListSaleHandlerTests"/> class.
	/// Sets up the test dependencies and lists fake data generators.
	/// </summary>
	public ListSaleHandlerTests()
	{
		_saleRepository = Substitute.For<ISaleRepository>();

		var config = new MapperConfiguration(cfg => cfg.CreateMap<Sale, ListSaleResult>());

		_mapper = config.CreateMapper();

		_handler = new ListSaleHandler(
			_saleRepository,
			_mapper);
	}

	/// <summary>
	/// Tests that a valid sale listing request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid sale data When listing sale Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = ListSaleHandlerTestData.GenerateValidCommand();
		var sales = ListSaleHandlerTestData.GenerateValidSalesList();

		_saleRepository.GetAll(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>())
			.Returns(sales);

		// When
		var listSaleResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		await _saleRepository.Received(1).GetAll(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());

		listSaleResult.Should().NotBeNull();
		listSaleResult.Sales.Should().NotBeNull();
		listSaleResult.Sales.Count.Should().Be(sales.Count);
		listSaleResult.Sales.First().Should().BeEquivalentTo(sales.First());
		listSaleResult.Sales.Last().Should().BeEquivalentTo(sales.Last());
	}

	/// <summary>
	/// Tests that an empty list of sales throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Empty list When listing sale Then throws KeyNotFoundException exception")]
	public async Task Handle_EmptyList_ThrowsValidationException()
	{
		// Given
		var command = new ListSaleCommand(); // Empty command will fail validation

		_saleRepository.GetAll()
			.Returns(new List<Sale>());

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<KeyNotFoundException>();
	}
}
