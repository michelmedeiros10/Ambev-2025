using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class GetProductHandlerTestData
{
	/// <summary>
	/// Configures the Faker to generate valid Product entities.
	/// The generated products will have valid:
	/// </summary>
	private static readonly Faker<GetProductCommand> getProductCommandFaker = new Faker<GetProductCommand>()
		.CustomInstantiator(c => new GetProductCommand(Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710")));

	private static readonly Faker<Product> getProductFaker = new Faker<Product>()
		.RuleFor(u => u.Title, f => f.Name.Random.AlphaNumeric(50))
		.RuleFor(u => u.Price, f => f.System.Random.Decimal(1, 10));

	/// <summary>
	/// Generates a valid Product entity with randomized data.
	/// The generated product will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid Product entity with randomly generated data.</returns>
	public static GetProductCommand GenerateValidCommand()
	{
		return getProductCommandFaker.Generate();
	}

	/// <summary>
	/// Generates a valid Product entity with randomized data.
	/// The generated entity will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid Product entity with randomly generated data.</returns>
	public static Product GenerateValidProduct()
	{
		return getProductFaker.Generate();
	}
}
