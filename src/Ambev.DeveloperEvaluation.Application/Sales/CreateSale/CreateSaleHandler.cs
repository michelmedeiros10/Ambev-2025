using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Events.Users;
using Ambev.DeveloperEvaluation.Domain.Events.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler for processing CreateSaleCommand requests
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
	private readonly ISaleRepository _saleRepository;
	private readonly IMapper _mapper;
	private readonly IEventBus _eventBus;

	/// <summary>
	/// Initializes a new instance of CreateSaleHandler
	/// </summary>
	/// <param name="saleRepository">The sale repository</param>
	/// <param name="mapper">The AutoMapper instance</param>
	public CreateSaleHandler(
		ISaleRepository saleRepository, 
		IMapper mapper, 
		IEventBus eventBus)
	{
		_saleRepository = saleRepository;
		_mapper = mapper;
		_eventBus = eventBus;
	}

	/// <summary>
	/// Handles the CreateSaleCommand request
	/// </summary>
	/// <param name="command">The CreateSale command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created sale details</returns>
	public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
	{
		var existingSale = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
		if (existingSale != null)
			throw new InvalidOperationException($"Sale with number '{command.SaleNumber}' already exists");

		var validator = new CreateSaleCommandValidator();
		var validationResult = await validator.ValidateAsync(command, cancellationToken);

		if (!validationResult.IsValid)
		{
			throw new ValidationException(validationResult.Errors);
		}

		//Grouping the products to check if there is some item with more than 20 units per product
		var sumQuantity = command.Products.GroupBy(p => p.ProductId)
			.Select(grp => new
			{
				grp.First().ProductId,
				TotalCount = grp.Sum(a => a.Quantity)
			}).ToList();

		var limit20 = sumQuantity.Any(q => q.TotalCount > 20);
		if (limit20)
		{
			throw new ValidationException("Maximum limit: 20 items per product");
		}

		//--------------
		//Discount rules
		//--------------
		foreach (var productSum in sumQuantity)
		{
			//Rule for 10% of discount (between 4 and 9)
			if (productSum.TotalCount >= 4
				&& productSum.TotalCount < 10)
			{
				foreach (var saleProduct in command.Products)
				{
					if (saleProduct.ProductId == productSum.ProductId)
					{
						saleProduct.Discount = (saleProduct.Price * saleProduct.Quantity) * 0.1M;
						saleProduct.TotalAmount = (saleProduct.Price * saleProduct.Quantity) * 0.9M;
					}
				}
			}

			//Rule for 20% of discount (between 10 and 20)
			if (productSum.TotalCount >= 10
				&& productSum.TotalCount <= 20)
			{
				foreach (var saleProduct in command.Products)
				{
					if (saleProduct.ProductId == productSum.ProductId)
					{
						saleProduct.Discount = (saleProduct.Price * saleProduct.Quantity) * 0.2M;
						saleProduct.TotalAmount = (saleProduct.Price * saleProduct.Quantity) * 0.8M;
					}
				}
			}
		}

		//Update the sale amount
		command.Amount = command.Products.Sum(p => p.TotalAmount);

		var sale = _mapper.Map<Sale>(command);

		var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

		var result = _mapper.Map<CreateSaleResult>(createdSale);

		//Create the event
		var ev = new SaleRegisteredEvent(sale)
		{
			Oid = Guid.Empty,
			Sid = Guid.NewGuid()
		};

		//Publish the event
		await _eventBus.PublishAsync(ev, cancellationToken);

		return result;
	}
}
