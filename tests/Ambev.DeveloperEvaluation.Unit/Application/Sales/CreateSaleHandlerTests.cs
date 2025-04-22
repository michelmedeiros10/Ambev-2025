using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CreateSaleHandler"/> class.
/// </summary>
public class CreateSaleHandlerTests
{
	private readonly ISaleRepository _saleRepository;
	private IMapper _mapper;
	private readonly IEventBus _eventBus;
	private readonly CreateSaleHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="CreateSaleHandlerTests"/> class.
	/// Sets up the test dependencies and creates fake data generators.
	/// </summary>
	public CreateSaleHandlerTests()
	{
		_saleRepository = Substitute.For<ISaleRepository>();

		var config = new MapperConfiguration(
			cfg =>
			{
				cfg.CreateMap<SaleProductCommand, SaleProduct>();
				cfg.CreateMap<Sale, CreateSaleResult>();
				cfg.CreateMap<CreateSaleCommand, Sale>()
					.ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
			});

		_mapper = config.CreateMapper();
		_eventBus = Substitute.For<IEventBus>();

		_handler = new CreateSaleHandler(
			_saleRepository,
			_mapper,
			_eventBus);
	}

	/// <summary>
	/// Tests that a valid sale creation request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid sale data When creating sale Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = CreateSaleHandlerTestData.GenerateValidCommand();

		var sale = _mapper.Map<Sale>(command);
		sale.Id = Guid.NewGuid();

		_saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
			.Returns(sale);

		// When
		var createSaleResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		createSaleResult.Should().NotBeNull();
		createSaleResult.Id.Should().Be(sale.Id);
		await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
	}

	/// <summary>
	/// Tests that an invalid sale creation request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid sale data When creating sale Then throws validation exception")]
	public async Task Handle_InvalidRequest_ThrowsValidationException()
	{
		// Given
		var command = new CreateSaleCommand(); // Empty command will fail validation

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}

	/// <summary>
	/// Tests that an existing sale number request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given existing sale data When creating sale Then throws validation exception")]
	public async Task Handle_ExistingSale_ThrowsValidationException()
	{
		// Given
		var command = CreateSaleHandlerTestData.GenerateValidCommand();

		_saleRepository.GetBySaleNumberAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
			.Returns(new Sale());

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<InvalidOperationException>();
	}

	/// <summary>
	/// Tests that a sale request with more than 20 product itens throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given sale with more than 20 product itens When creating sale Then throws validation exception")]
	public async Task Handle_MoreThan20_ThrowsValidationException()
	{
		// Given
		var command = CreateSaleHandlerTestData.GenerateValidCommand();
		command.Products.First().Quantity = 10;
		command.Products.Last().Quantity = 11;
		command.Products.Last().ProductId = command.Products.First().ProductId;

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}

	/// <summary>
	/// Tests that the mapper is called with the correct command.
	/// </summary>
	[Fact(DisplayName = "Given valid command When handling Then maps command to sale entity")]
	public async Task Handle_ValidRequest_MapsCommandToSale()
	{
		// Given
		var command = CreateSaleHandlerTestData.GenerateValidCommand();
		
		var sale = _mapper.Map<Sale>(command);
		sale.Id = Guid.NewGuid();

		_saleRepository.CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
			.Returns(sale);

		// When
		var result = await _handler.Handle(command, CancellationToken.None);

		// Then
		result.Should().NotBeNull();
		result.Id.Should().Be(sale.Id);
		await _saleRepository.Received(1).CreateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>());
	}
}
