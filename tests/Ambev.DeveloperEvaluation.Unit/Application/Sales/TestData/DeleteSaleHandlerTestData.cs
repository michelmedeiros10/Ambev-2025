using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteSaleHandlerTestData
{
	/// <summary>
	/// Configures the Faker to generate valid Sale entities.
	/// The generated sales will have valid:
	/// - Id (fixed)
	/// </summary>
	private static readonly Faker<DeleteSaleCommand> deleteSaleCommandFaker = new Faker<DeleteSaleCommand>()
		.CustomInstantiator(c => new DeleteSaleCommand(Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710")));

	/// <summary>
	/// Generates a valid Sale entity with randomized data.
	/// The generated sale will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid Sale entity with randomly generated data.</returns>
	public static DeleteSaleCommand GenerateValidCommand()
	{
		return deleteSaleCommandFaker.Generate();
	}
}
