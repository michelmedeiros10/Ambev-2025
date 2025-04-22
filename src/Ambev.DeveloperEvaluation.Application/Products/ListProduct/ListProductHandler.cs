using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProduct;

/// <summary>
/// Handler for processing ListProductCommand requests
/// </summary>
public class ListProductHandler : IRequestHandler<ListProductCommand, ListProductResult>
{
	private readonly IProductRepository _productRepository;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initializes a new instance of ListProductHandler
	/// </summary>
	/// <param name="productRepository">The product repository</param>
	/// <param name="mapper">The AutoMapper instance</param>
	/// <param name="validator">The validator for ListProductCommand</param>
	public ListProductHandler(
		IProductRepository productRepository,
		IMapper mapper)
	{
		_productRepository = productRepository;
		_mapper = mapper;
	}

	/// <summary>
	/// Handles the ListProductCommand request
	/// </summary>
	/// <param name="request">The ListProduct command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The product list if found</returns>
	public async Task<ListProductResult> Handle(ListProductCommand request, CancellationToken cancellationToken)
	{
		var validator = new ListProductValidator();
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
			throw new ValidationException(validationResult.Errors);

		var products = await _productRepository.GetAll(request.SortOrder, request.PageNumber, request.PageSize);

		if (products == null || !products.Any())
			throw new KeyNotFoundException($"Products were not found");

		return new ListProductResult { Products = products };
	}
}
