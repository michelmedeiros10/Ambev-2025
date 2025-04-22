using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events.Users;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

/// <summary>
/// Handler for processing DeleteSaleCommand requests
/// </summary>
public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
	private readonly ISaleRepository _saleRepository;
	private readonly IEventBus _eventBus;

	/// <summary>
	/// Initializes a new instance of DeleteSaleHandler
	/// </summary>
	/// <param name="saleRepository">The sale repository</param>
	/// <param name="validator">The validator for DeleteSaleCommand</param>
	public DeleteSaleHandler(
		ISaleRepository saleRepository, 
		IEventBus eventBus)
	{
		_saleRepository = saleRepository;
		_eventBus = eventBus;
	}

	/// <summary>
	/// Handles the DeleteSaleCommand request
	/// </summary>
	/// <param name="request">The DeleteSale command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The result of the delete operation</returns>
	public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
	{
		var validator = new DeleteSaleValidator();
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
			throw new ValidationException(validationResult.Errors);

		var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);
		if (!success)
			throw new KeyNotFoundException($"Sale with ID {request.Id} not found");

		//Create the event
		var ev = new SaleDeletedEvent(request.Id.ToString())
		{
			Oid = Guid.Empty,
			Sid = Guid.NewGuid()
		};

		//Publish the event
		await _eventBus.PublishAsync(ev, cancellationToken);

		return new DeleteSaleResponse { Success = true };
	}
}
