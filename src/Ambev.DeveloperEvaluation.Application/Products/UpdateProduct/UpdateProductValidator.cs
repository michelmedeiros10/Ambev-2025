using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand that defines validation rules for product updation command.
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
	/// <summary>
	/// Initializes a new instance of the UpdateProductCommandValidator with defined validation rules.
	/// </summary>
	/// <remarks>
	/// Validation rules include:
	/// </remarks>
	public UpdateProductCommandValidator()
	{
		RuleFor(p => p.Title).NotEmpty().Length(3, 100);
		RuleFor(p => p.Price).GreaterThan(0);
	}
}