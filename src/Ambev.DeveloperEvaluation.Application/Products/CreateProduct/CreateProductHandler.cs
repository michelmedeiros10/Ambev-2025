using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Products;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing CreateProductCommand requests
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;

	/// <summary>
	/// Initializes a new instance of CreateProductHandler
	/// </summary>
	/// <param name="productRepository">The product repository</param>
	/// <param name="mapper">The AutoMapper instance</param>
	public CreateProductHandler(
		IProductRepository productRepository, 
		IMapper mapper, 
		IEventBus eventBus)
	{
		_productRepository = productRepository;
		_mapper = mapper;
		_eventBus = eventBus;
	}

	/// <summary>
	/// Handles the CreateProductCommand request
	/// </summary>
	/// <param name="command">The CreateProduct command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created product details</returns>
	public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
	{
		var existingProduct = await _productRepository.GetByTitleAsync(command.Title, cancellationToken);
		if (existingProduct != null)
			throw new InvalidOperationException($"Product with title '{command.Title}' already exists");

		var validator = new CreateProductCommandValidator();
		var validationResult = await validator.ValidateAsync(command, cancellationToken);

		if (!validationResult.IsValid)
			throw new ValidationException(validationResult.Errors);

		var product = _mapper.Map<Product>(command);

		var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);

		var result = _mapper.Map<CreateProductResult>(createdProduct);

		//Create the event
		var ev = new ProductRegisteredEvent(product)
		{
			Oid = Guid.Empty,
			Sid = Guid.NewGuid()
		};

		//Publish the event
		await _eventBus.PublishAsync(ev, cancellationToken);

		return result;
	}
}
