﻿using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

/// <summary>
/// Contains unit tests for the <see cref="CreateProductHandler"/> class.
/// </summary>
public class CreateProductHandlerTests
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;
	private readonly CreateProductHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="CreateProductHandlerTests"/> class.
	/// Sets up the test dependencies and creates fake data generators.
	/// </summary>
	public CreateProductHandlerTests()
	{
		_productRepository = Substitute.For<IProductRepository>();
		_mapper = Substitute.For<IMapper>();
		_eventBus = Substitute.For<IEventBus>();
		_handler = new CreateProductHandler(
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
		var command = CreateProductHandlerTestData.GenerateValidCommand();
		var product = new Product
		{
			Id = Guid.NewGuid(),
			Category = command.Category,
			Count = command.Count,
			Description = command.Description,
			Image = command.Image,
			Price = command.Price,
			Rate = command.Rate,
			Title = command.Title
		};

		var result = new CreateProductResult
		{
			Id = product.Id,
		};

		_mapper.Map<Product>(command).Returns(product);
		_mapper.Map<CreateProductResult>(product).Returns(result);

		_productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
			.Returns(product);

		// When
		var createProductResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		createProductResult.Should().NotBeNull();
		createProductResult.Id.Should().Be(product.Id);
		await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
	}

	/// <summary>
	/// Tests that an invalid product creation request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
	public async Task Handle_InvalidRequest_ThrowsValidationException()
	{
		// Given
		var command = new CreateProductCommand(); // Empty command will fail validation

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}

	/// <summary>
	/// Tests that an existing product email request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given existing product data When creating product Then throws validation exception")]
	public async Task Handle_ExistingProduct_ThrowsValidationException()
	{
		// Given
		var command = CreateProductHandlerTestData.GenerateValidCommand();

		_productRepository.GetByTitleAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
			.Returns(new Product());

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<InvalidOperationException>();
	}

	/// <summary>
	/// Tests that the mapper is called with the correct command.
	/// </summary>
	[Fact(DisplayName = "Given valid command When handling Then maps command to product entity")]
	public async Task Handle_ValidRequest_MapsCommandToProduct()
	{
		// Given
		var command = CreateProductHandlerTestData.GenerateValidCommand();
		var product = new Product
		{
			Id = Guid.NewGuid(),
			Category = command.Category,
			Count = command.Count,
			Description = command.Description,
			Image = command.Image,
			Price = command.Price,
			Rate = command.Rate,
			Title = command.Title
		};

		_mapper.Map<Product>(command).Returns(product);
		_productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
			.Returns(product);

		// When
		await _handler.Handle(command, CancellationToken.None);

		// Then
		_mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(c =>
			c.Category == command.Category &&
			c.Count == command.Count &&
			c.Description == command.Description &&
			c.Image == command.Image &&
			c.Price == command.Price &&
			c.Rate == command.Rate &&
			c.Title == command.Title));
	}
}
