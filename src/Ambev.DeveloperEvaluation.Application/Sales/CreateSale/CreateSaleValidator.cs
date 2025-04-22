using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleCommand that defines validation rules for sale creation command.
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
	/// <summary>
	/// Initializes a new instance of the CreateSaleCommandValidator with defined validation rules.
	/// </summary>
	/// <remarks>
	/// Validation rules include:
	/// </remarks>
	public CreateSaleCommandValidator()
	{
		RuleFor(s => s.Branch).NotEmpty().Length(3, 50);
		RuleFor(s => s.Products).NotNull().NotEmpty();
		RuleFor(s => s.CustomerId).NotEmpty();
		RuleFor(s => s.SaleNumber).GreaterThan(0);
		RuleFor(s => s.Amount).GreaterThan(0);
		RuleForEach(s => s.Products).ChildRules(c => c.RuleFor(p => p.Quantity).GreaterThan(0));
		RuleForEach(s => s.Products).ChildRules(c => c.RuleFor(p => p.Quantity).LessThan(21).WithMessage("Maximum limit: 20 items per product"));
	}
}