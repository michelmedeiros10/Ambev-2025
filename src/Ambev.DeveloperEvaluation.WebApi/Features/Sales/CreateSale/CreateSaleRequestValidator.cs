using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for CreateSaleRequest that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
	/// <summary>
	/// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
	/// </summary>
	/// <remarks>
	/// Validation rules include:
	/// </remarks>
	public CreateSaleRequestValidator()
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