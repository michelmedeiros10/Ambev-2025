using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductRequest that defines validation rules for product updation.
/// </summary>
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
	/// <summary>
	/// Initializes a new instance of the UpdateProductRequestValidator with defined validation rules.
	/// </summary>
	/// <remarks>
	/// Validation rules include:
	/// </remarks>
	public UpdateProductRequestValidator()
	{
		RuleFor(p => p.Title).NotEmpty().Length(3, 100);
		RuleFor(p => p.Price).GreaterThan(0);
	}
}