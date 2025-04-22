using Ambev.DeveloperEvaluation.Application.Users.ListUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ListUserHandlerTestData
{
	/// <summary>
	/// Configures the Faker to generate valid User entities.
	/// The generated users will have valid:
	/// </summary>
	private static readonly Faker<ListUserCommand> listUserCommandFaker = new Faker<ListUserCommand>()
		.RuleFor(u => u.PageNumber, 1)
		.RuleFor(u => u.PageSize, 10)
		.RuleFor(u => u.SortOrder, "");

	/// <summary>
	/// Generates a valid User entity with randomized data.
	/// The generated user will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid User entity with randomly generated data.</returns>
	public static ListUserCommand GenerateValidCommand()
	{
		return listUserCommandFaker.Generate();
	}

	/// <summary>
	/// Generates a list of user entity
	/// </summary>
	/// <returns></returns>
	public static List<User> GenerateValidUsersList()
	{
		var result = new Faker<User>().Generate(3);
		return result;
	}
}
