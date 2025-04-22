﻿using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class GetUserHandlerTestData
{
	/// <summary>
	/// Configures the Faker to generate valid User entities.
	/// The generated users will have valid:
	/// - Id (fixed)
	/// </summary>
	private static readonly Faker<GetUserCommand> getUserCommandFaker = new Faker<GetUserCommand>()
		.CustomInstantiator(c => new GetUserCommand(Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710")));

	private static readonly Faker<User> getUserFaker = new Faker<User>()
	.RuleFor(u => u.Username, f => f.Internet.UserName())
	.RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
	.RuleFor(u => u.Email, f => f.Internet.Email())
	.RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
	.RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
	.RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin));

	/// <summary>
	/// Generates a valid User command with randomized data.
	/// The generated command will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid User command with randomly generated data.</returns>
	public static GetUserCommand GenerateValidCommand()
	{
		return getUserCommandFaker.Generate();
	}

	/// <summary>
	/// Generates a valid User entity with randomized data.
	/// The generated entity will have all properties populated with valid values
	/// that meet the system's validation requirements.
	/// </summary>
	/// <returns>A valid User entity with randomly generated data.</returns>
	public static User GenerateValidUser()
	{
		return getUserFaker.Generate();
	}
}
