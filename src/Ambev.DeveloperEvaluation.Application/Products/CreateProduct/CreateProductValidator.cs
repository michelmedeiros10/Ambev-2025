using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation command.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
	/// <summary>
	/// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
	/// </summary>
	/// <remarks>
	/// Validation rules include:
	/// </remarks>
	public CreateProductCommandValidator()
	{
		RuleFor(p => p.Title).NotEmpty().Length(3, 100);
		RuleFor(p => p.Price).GreaterThan(0);
	}
}