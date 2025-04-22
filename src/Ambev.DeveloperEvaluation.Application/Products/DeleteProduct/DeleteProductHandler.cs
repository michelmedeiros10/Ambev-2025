using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events.Products;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Handler for processing DeleteProductCommand requests
/// </summary>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
{
	private readonly IProductRepository _productRepository;
	private readonly IEventBus _eventBus;

	/// <summary>
	/// Initializes a new instance of DeleteProductHandler
	/// </summary>
	/// <param name="productRepository">The product repository</param>
	/// <param name="validator">The validator for DeleteProductCommand</param>
	public DeleteProductHandler(
		IProductRepository productRepository, IEventBus eventBus)
	{
		_productRepository = productRepository;
		_eventBus = eventBus;
	}

	/// <summary>
	/// Handles the DeleteProductCommand request
	/// </summary>
	/// <param name="request">The DeleteProduct command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The result of the delete operation</returns>
	public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
	{
		var validator = new DeleteProductValidator();
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
			throw new ValidationException(validationResult.Errors);

		var success = await _productRepository.DeleteAsync(request.Id, cancellationToken);
		if (!success)
			throw new KeyNotFoundException($"Product with ID {request.Id} not found");

		//Create the event
		var ev = new ProductDeletedEvent(request.Id.ToString())
		{
			Oid = Guid.Empty,
			Sid = Guid.NewGuid()
		};

		//Publish the event
		await _eventBus.PublishAsync(ev, cancellationToken);

		return new DeleteProductResponse { Success = true };
	}
}
