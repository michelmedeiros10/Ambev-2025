using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
	/// <summary>
	/// Configures the Faker to generate valid Sale entities.
	/// The generated sales will have valid:
	/// </summary>
	private static readonly Faker<CreateSaleCommand> createSaleCommandFaker = new Faker<CreateSaleCommand>()
		.RuleFor(u => u.CustomerId, Guid.NewGuid())
		.RuleFor(u => u.Branch, f => f.Name.Random.AlphaNumeric(50))
		.RuleFor(u => u.SaleNumber, f => f.System.Random.Number(1, 10))
		.RuleFor(u => u.Amount, f => f.Name.Random.Decimal(1, 10))		
		;

	private static readonly Faker<SaleProductCommand> createProductFaker = new Faker<SaleProductCommand>()
		.RuleFor(p => p.Price, f => f.System.Random.Decimal(1, 2))
		.RuleFor(p => p.ProductId, f => f.System.Random.Guid().ToString())
		.RuleFor(p => p.TotalAmount, f => f.System.Random.Decimal(1, 2))
		.RuleFor(p => p.Quantity, f => f.System.Random.Number(1, 2))
		;

	/// <summary>
	/// Generates a valid Sale entity with randomized data.
	/// The generated sale will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid Sale entity with randomly generated data.</returns>
	public static CreateSaleCommand GenerateValidCommand()
	{
		var command = createSaleCommandFaker.Generate();
		command.Products = createProductFaker.Generate(3);

		return command;
	}
}
