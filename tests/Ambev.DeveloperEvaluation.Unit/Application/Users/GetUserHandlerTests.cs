using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

/// <summary>
/// Contains unit tests for the <see cref="GetUserHandler"/> class.
/// </summary>
public class GetUserHandlerTests
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	private readonly GetUserHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="GetUserHandlerTests"/> class.
	/// Sets up the test dependencies and gets fake data generators.
	/// </summary>
	public GetUserHandlerTests()
	{
		_userRepository = Substitute.For<IUserRepository>();

		var config = new MapperConfiguration(cfg => cfg.CreateMap<User, GetUserResult>());
		_mapper = config.CreateMapper();

		_handler = new GetUserHandler(
			_userRepository,
			_mapper);
	}

	/// <summary>
	/// Tests that a valid user getting request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid user data When getting user Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = GetUserHandlerTestData.GenerateValidCommand();
		var user = GetUserHandlerTestData.GenerateValidUser();

		_userRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(user);

		// When
		var getUserResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		await _userRepository.Received(1).GetByIdAsync(Arg.Any<Guid>());

		getUserResult.Should().NotBeNull();
		getUserResult.Id.Should().Be(user.Id);
		getUserResult.Email.Should().Be(user.Email);
		getUserResult.Phone.Should().Be(user.Phone);
		getUserResult.Role.Should().Be(user.Role);
		getUserResult.Status.Should().Be(user.Status);
	}

	/// <summary>
	/// Tests that an invalid user deleting request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid user data When deleting user Then throws validation exception")]
	public async Task Handle_InvalidRequest_ThrowsValidationException()
	{
		// Given
		var command = new GetUserCommand(Guid.Empty); // Empty command will fail validation

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}

	/// <summary>
	/// Tests that an invalid user Id request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid user Id data When getting user Then throws validation exception")]
	public async Task Handle_InvalidId_ThrowsValidationException()
	{
		// Given
		var command = new GetUserCommand(Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710")); // Empty command will fail validation

		_userRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(default(User));

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<KeyNotFoundException>();
	}
}
