using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Events.Products;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing UpdateProductCommand requests
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;

	/// <summary>
	/// Initializes a new instance of UpdateProductHandler
	/// </summary>
	/// <param name="productRepository">The product repository</param>
	/// <param name="mapper">The AutoMapper instance</param>
	public UpdateProductHandler(IProductRepository productRepository, IMapper mapper, IEventBus eventBus)
	{
		_productRepository = productRepository;
		_mapper = mapper;
		_eventBus = eventBus;
	}

	/// <summary>
	/// Handles the UpdateProductCommand request
	/// </summary>
	/// <param name="command">The UpdateProduct command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The updated product details</returns>
	public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
	{
		var existProduct = await _productRepository.GetByIdAsync(Guid.Parse(command.Id));
		if (existProduct == null)
		{
			throw new InvalidOperationException($"Product with Id:[{command.Id}] was not found");
		}

		var validator = new UpdateProductCommandValidator();
		var validationResult = await validator.ValidateAsync(command, cancellationToken);

		if (!validationResult.IsValid)
			throw new ValidationException(validationResult.Errors);

		var product = _mapper.Map<Product>(command);

		_mapper.Map(product, existProduct);

		var updatedProduct = _productRepository.Update(product);

		var result = _mapper.Map<UpdateProductResult>(updatedProduct);

		//Create the event
		var ev = new ProductUpdatedEvent(product)
		{
			Oid = Guid.Empty,
			Sid = Guid.NewGuid()
		};

		//Publish the event
		await _eventBus.PublishAsync(ev, cancellationToken);

		return result;
	}
}
