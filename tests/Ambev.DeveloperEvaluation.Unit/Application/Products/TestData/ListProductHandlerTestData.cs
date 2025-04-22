using Ambev.DeveloperEvaluation.Application.Products.ListProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ListProductHandlerTestData
{
	/// <summary>
	/// Configures the Faker to generate valid Product entities.
	/// The generated products will have valid:
	/// </summary>
	private static readonly Faker<ListProductCommand> listProductCommandFaker = new Faker<ListProductCommand>()
		.RuleFor(u => u.PageNumber, 1)
		.RuleFor(u => u.PageSize, 10)
		.RuleFor(u => u.SortOrder, "");

	/// <summary>
	/// Generates a valid Product entity with randomized data.
	/// The generated product will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid Product entity with randomly generated data.</returns>
	public static ListProductCommand GenerateValidCommand()
	{
		return listProductCommandFaker.Generate();
	}

	/// <summary>
	/// Generates a list of product entity
	/// </summary>
	/// <returns></returns>
	public static List<Product> GenerateValidProductsList()
	{
		var result = new Faker<Product>().Generate(3);
		return result;
	}
}
