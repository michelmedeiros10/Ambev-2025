using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteUserHandlerTestData
{
	/// <summary>
	/// Configures the Faker to generate valid User entities.
	/// The generated users will have valid:
	/// - Id (fixed)
	/// </summary>
	private static readonly Faker<DeleteUserCommand> deleteUserCommandFaker = new Faker<DeleteUserCommand>()
		.CustomInstantiator(c => new DeleteUserCommand(Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710")));

	/// <summary>
	/// Generates a valid User entity with randomized data.
	/// The generated user will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid User entity with randomly generated data.</returns>
	public static DeleteUserCommand GenerateValidCommand()
	{
		return deleteUserCommandFaker.Generate();
	}
}
