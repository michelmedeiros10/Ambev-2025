using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Service;
using Ambev.DeveloperEvaluation.Domain.Events.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEventBus _eventBus;

	/// <summary>
	/// Initializes a new instance of CreateUserHandler
	/// </summary>
	/// <param name="userRepository">The user repository</param>
	/// <param name="mapper">The AutoMapper instance</param>
	/// <param name="validator">The validator for CreateUserCommand</param>
	public CreateUserHandler(
		IUserRepository userRepository,
		IMapper mapper,
		IPasswordHasher passwordHasher,
		IEventBus eventBus)
	{
		_userRepository = userRepository;
		_mapper = mapper;
		_passwordHasher = passwordHasher;
		_eventBus = eventBus;
	}

	/// <summary>
	/// Handles the CreateUserCommand request
	/// </summary>
	/// <param name="command">The CreateUser command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created user details</returns>
	public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
		var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
		if (existingUser != null)
			throw new InvalidOperationException($"User with email '{command.Email}' already exists");
		
        var validator = new CreateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var user = _mapper.Map<User>(command);
        user.Password = _passwordHasher.HashPassword(command.Password);

        var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
        var result = _mapper.Map<CreateUserResult>(createdUser);

		//Create the event
		var ev = new UserRegisteredEvent(user) 
		{ 
			Oid = Guid.Empty, 
			Sid = Guid.NewGuid() 
		};

		//Publish the event
		await _eventBus.PublishAsync(ev, cancellationToken);

		return result;
    }
}
