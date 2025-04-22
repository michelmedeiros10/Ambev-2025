using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteProductHandlerTestData
{
	/// <summary>
	/// Configures the Faker to generate valid Product entities.
	/// The generated products will have valid:
	/// - Id (fixed)
	/// </summary>
	private static readonly Faker<DeleteProductCommand> deleteProductCommandFaker = new Faker<DeleteProductCommand>()
		.CustomInstantiator(c => new DeleteProductCommand(Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710")));

	/// <summary>
	/// Generates a valid Product entity with randomized data.
	/// The generated product will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid Product entity with randomly generated data.</returns>
	public static DeleteProductCommand GenerateValidCommand()
	{
		return deleteProductCommandFaker.Generate();
	}
}
