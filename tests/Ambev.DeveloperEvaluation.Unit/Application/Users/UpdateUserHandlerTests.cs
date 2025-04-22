using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Users.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Users;

/// <summary>
/// Contains unit tests for the <see cref="UpdateUserHandler"/> class.
/// </summary>
public class UpdateUserHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
	private readonly IEventBus _eventBus;
	private readonly UpdateUserHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserHandlerTests"/> class.
    /// Sets up the test dependencies and updates fake data generators.
    /// </summary>
    public UpdateUserHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();

		_mapper = Substitute.For<IMapper>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
		_eventBus = Substitute.For<IEventBus>();
		_handler = new UpdateUserHandler(
            _userRepository,
            _mapper,
            _passwordHasher,
            _eventBus);
    }

    /// <summary>
    /// Tests that a valid user updating request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid user data When updating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var user = new User
        {
            Id = Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710"),
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role
        };

        var result = new UpdateUserResult
        {
            Id = user.Id,
        };

        _mapper.Map<User>(command).Returns(user);
        _mapper.Map<UpdateUserResult>(user).Returns(result);

        _userRepository.GetByIdAsync(Arg.Any<Guid>())
            .Returns(user);
        _userRepository.Update(Arg.Any<User>())
            .Returns(user);

        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        // When
        var updateUserResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateUserResult.Should().NotBeNull();
        updateUserResult.Id.Should().Be(user.Id);
        _userRepository.Received(1).Update(Arg.Any<User>());
    }

    /// <summary>
    /// Tests that an invalid user updating request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid user data When updating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new UpdateUserCommand { Id = "218d4588-be6d-4fe9-8278-bf3a43914710" }; // Empty command will fail validation
		
        _userRepository.GetByIdAsync(Arg.Any<Guid>())
	        .Returns(default(User));

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

	/// <summary>
	/// Tests that an invalid user Id request throws a validation exception.
	/// </summary>
	[Fact(DisplayName = "Given invalid user Id data When updating user Then throws validation exception")]
	public async Task Handle_InvalidId_ThrowsValidationException()
	{
		// Given
		var command = new UpdateUserCommand { Id = "218d4588-be6d-4fe9-8278-bf3a43914710" }; // Empty command will fail validation

		_userRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(new User());

		// When
		var act = () => _handler.Handle(command, CancellationToken.None);

		// Then
		await act.Should().ThrowAsync<FluentValidation.ValidationException>();
	}

	/// <summary>
	/// Tests that the password is hashed before saving the user.
	/// </summary>
	[Fact(DisplayName = "Given user updating request When handling Then password is hashed")]
    public async Task Handle_ValidRequest_HashesPassword()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var originalPassword = command.Password;
        const string hashedPassword = "h@shedPassw0rd";
        var user = new User
        {
            Id = Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710"),
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role
        };

        _mapper.Map<User>(command).Returns(user);
        _userRepository.GetByIdAsync(Arg.Any<Guid>())
            .Returns(user);
        _userRepository.Update(Arg.Any<User>())
            .Returns(user);
        _passwordHasher.HashPassword(originalPassword).Returns(hashedPassword);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _passwordHasher.Received(1).HashPassword(originalPassword);
        _userRepository.Received(1).Update(
            Arg.Is<User>(u => u.Password == hashedPassword));
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        // Given
        var command = UpdateUserHandlerTestData.GenerateValidCommand();
        var user = new User
        {
            Id = Guid.Parse("218d4588-be6d-4fe9-8278-bf3a43914710"),
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role
        };

        _mapper.Map<User>(command).Returns(user);
        _userRepository.GetByIdAsync(Arg.Any<Guid>())
            .Returns(user);
        _userRepository.Update(Arg.Any<User>())
            .Returns(user);
        _passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<User>(Arg.Is<UpdateUserCommand>(c =>
            c.Username == command.Username &&
            c.Email == command.Email &&
            c.Phone == command.Phone &&
            c.Status == command.Status &&
            c.Role == command.Role));
    }
}
