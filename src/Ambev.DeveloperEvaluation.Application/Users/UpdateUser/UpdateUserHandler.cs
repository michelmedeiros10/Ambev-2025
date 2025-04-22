using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Events.Products;
using Ambev.DeveloperEvaluation.Domain.Events.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Handler for processing UpdateUserCommand requests
/// </summary>
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	private readonly IPasswordHasher _passwordHasher;
	private readonly IEventBus _eventBus;

	/// <summary>
	/// Initializes a new instance of UpdateUserHandler
	/// </summary>
	/// <param name="userRepository">The user repository</param>
	/// <param name="mapper">The AutoMapper instance</param>
	/// <param name="passwordHasher">The PasswordHasher instance</param>
	/// <param name="eventBus">The EventBus instance</param>
	public UpdateUserHandler(
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
	/// Handles the UpdateUserCommand request
	/// </summary>
	/// <param name="command">The UpdateUser command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The updated user details</returns>
	public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
	{
		var existUser = await _userRepository.GetByIdAsync(Guid.Parse(command.Id));
		if (existUser == null)
		{
			throw new InvalidOperationException($"User with Id:[{command.Id}] was not found");
		}

		var validator = new UpdateUserCommandValidator();
		var validationResult = await validator.ValidateAsync(command, cancellationToken);

		if (!validationResult.IsValid)
			throw new ValidationException(validationResult.Errors);

		var user = _mapper.Map<User>(command);
		user.Password = _passwordHasher.HashPassword(command.Password);

		_mapper.Map(user, existUser);

		existUser.UpdatedAt = DateTime.UtcNow;

		var updatedUser = _userRepository.Update(existUser);
		var result = _mapper.Map<UpdateUserResult>(updatedUser);

		//TODO: Create DTOs to avoid cycle serialization
		if (updatedUser.Name != null)
			updatedUser.Name.User = null;
		if (updatedUser.Address != null)
			updatedUser.Address.User = null;

		//Create the event
		var ev = new UserUpdatedEvent(updatedUser)
		{
			Oid = Guid.Empty,
			Sid = Guid.NewGuid()
		};

		//Publish the event
		await _eventBus.PublishAsync(ev, cancellationToken);

		return result;
	}
}
