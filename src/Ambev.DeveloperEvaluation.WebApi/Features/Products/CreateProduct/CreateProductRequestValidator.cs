using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductRequest that defines validation rules for product creation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
	/// <summary>
	/// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
	/// </summary>
	/// <remarks>
	/// Validation rules include:
	/// </remarks>
	public CreateProductRequestValidator()
	{
		RuleFor(p => p.Title).NotEmpty().Length(3, 100);
		RuleFor(p => p.Price).GreaterThan(0);
	}
}