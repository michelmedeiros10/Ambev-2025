using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ListSaleHandlerTestData
{
	/// <summary>
	/// Configures the Faker to generate valid Sale entities.
	/// The generated sales will have valid:
	/// </summary>
	private static readonly Faker<ListSaleCommand> listSaleCommandFaker = new Faker<ListSaleCommand>()
		.RuleFor(u => u.PageNumber, 1)
		.RuleFor(u => u.PageSize, 10)
		.RuleFor(u => u.SortOrder, "");

	/// <summary>
	/// Generates a valid Sale entity with randomized data.
	/// The generated sale will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid Sale entity with randomly generated data.</returns>
	public static ListSaleCommand GenerateValidCommand()
	{
		return listSaleCommandFaker.Generate();
	}

	/// <summary>
	/// Generates a list of sale entity
	/// </summary>
	/// <returns></returns>
	public static List<Sale> GenerateValidSalesList()
	{
		var result = new Faker<Sale>().Generate(3);
		return result;
	}
}
