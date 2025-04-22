using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale;

/// <summary>
/// Handler for processing ListSaleCommand requests
/// </summary>
public class ListSaleHandler : IRequestHandler<ListSaleCommand, ListSaleResult>
{
	private readonly ISaleRepository _saleRepository;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initializes a new instance of ListSaleHandler
	/// </summary>
	/// <param name="saleRepository">The sale repository</param>
	/// <param name="mapper">The AutoMapper instance</param>
	/// <param name="validator">The validator for ListSaleCommand</param>
	public ListSaleHandler(
		ISaleRepository saleRepository,
		IMapper mapper)
	{
		_saleRepository = saleRepository;
		_mapper = mapper;
	}

	/// <summary>
	/// Handles the ListSaleCommand request
	/// </summary>
	/// <param name="request">The ListSale command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The sale list if found</returns>
	public async Task<ListSaleResult> Handle(ListSaleCommand request, CancellationToken cancellationToken)
	{
		var validator = new ListSaleValidator();
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
			throw new ValidationException(validationResult.Errors);

		var sales = await _saleRepository.GetAll(request.SortOrder, request.PageNumber, request.PageSize);
		
		if (sales == null || !sales.Any())
			throw new KeyNotFoundException($"Sales were not found");

		return new ListSaleResult { Sales = sales };
	}
}
