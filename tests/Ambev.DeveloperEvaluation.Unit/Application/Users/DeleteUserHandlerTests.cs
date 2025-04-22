using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

/// <summary>
/// Contains unit tests for the <see cref="DeleteUserHandler"/> class.
/// </summary>
public class DeleteUserHandlerTests
{
	private readonly IUserRepository _userRepository;
	private readonly IEventBus _eventBus;
	private readonly DeleteUserHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="DeleteUserHandlerTests"/> class.
	/// Sets up the test dependencies and deletes fake data generators.
	/// </summary>
	public DeleteUserHandlerTests()
	{
		_userRepository = Substitute.For<IUserRepository>();
		_eventBus = Substitute.For<IEventBus>();

		_handler = new DeleteUserHandler(
			_userRepository,
			_eventBus);
	}

	/// <summary>
	/// Tests that a valid user deleting request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid user data When deleting user Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = DeleteUserHandlerTestData.GenerateValidCommand();

		_userRepository.DeleteAsync(Arg.Any<Guid>())
			.Returns(true);

		// When
		var deleteUserResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		deleteUserResult.Should().NotBeNull();
		await _userRepository.Received(1).DeleteAsync(Arg.Any<Guid>());
	}

	/// <summary>
	/// Tests that an invalid user deleting request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid user data When deleting user Then throws validation exception")]
	public async Task Handle_InvalidRequest_ThrowsValidationException()
	{
		// Given
		var command = new DeleteUserCommand(Guid.Empty); // Empty command will fail validation

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}
}
