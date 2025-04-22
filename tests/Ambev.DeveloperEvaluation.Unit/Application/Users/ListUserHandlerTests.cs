using Ambev.DeveloperEvaluation.Application.Users.ListUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

/// <summary>
/// Contains unit tests for the <see cref="ListUserHandler"/> class.
/// </summary>
public class ListUserHandlerTests
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	private readonly ListUserHandler _handler;

	/// <summary>
	/// Initializes a new instance of the <see cref="ListUserHandlerTests"/> class.
	/// Sets up the test dependencies and lists fake data generators.
	/// </summary>
	public ListUserHandlerTests()
	{
		_userRepository = Substitute.For<IUserRepository>();

		var config = new MapperConfiguration(cfg => cfg.CreateMap<User, ListUserResult>());

		_mapper = config.CreateMapper();

		_handler = new ListUserHandler(
			_userRepository,
			_mapper);
	}

	/// <summary>
	/// Tests that a valid user listing request is handled successfully.
	/// </summary>
	[Fact(DisplayName = "Given valid user data When listing user Then returns success response")]
	public async Task Handle_ValidRequest_ReturnsSuccessResponse()
	{
		// Given
		var command = ListUserHandlerTestData.GenerateValidCommand();
		var users = ListUserHandlerTestData.GenerateValidUsersList();

		_userRepository.GetAll(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>())
			.Returns(users);

		// When
		var listUserResult = await _handler.Handle(command, CancellationToken.None);

		// Then
		await _userRepository.Received(1).GetAll(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>());

		listUserResult.Should().NotBeNull();
		listUserResult.Users.Should().NotBeNull();
		listUserResult.Users.Count.Should().Be(users.Count);
		listUserResult.Users.First().Should().BeEquivalentTo(users.First());
		listUserResult.Users.Last().Should().BeEquivalentTo(users.Last());
	}

	/// <summary>
	/// Tests that an empty list of users throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Empty list When listing user Then throws KeyNotFoundException exception")]
	public async Task Handle_EmptyList_ThrowsValidationException()
	{
		// Given
		var command = new ListUserCommand(); // Empty command will fail validation

		_userRepository.GetAll()
			.Returns(new List<User>());

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<KeyNotFoundException>();
	}
}
