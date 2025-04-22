using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

/// <summary>
/// Handler for processing DeleteUserCommand requests
/// </summary>
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IUserRepository _userRepository;
	private readonly IEventBus _eventBus;

	/// <summary>
	/// Initializes a new instance of DeleteUserHandler
	/// </summary>
	/// <param name="userRepository">The user repository</param>
	/// <param name="eventBus">The EventBus instance</param>
	public DeleteUserHandler(
		IUserRepository userRepository, 
		IEventBus eventBus)
	{
		_userRepository = userRepository;
		_eventBus = eventBus;
	}

	/// <summary>
	/// Handles the DeleteUserCommand request
	/// </summary>
	/// <param name="request">The DeleteUser command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The result of the delete operation</returns>
	public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteUserValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _userRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"User with ID {request.Id} not found");
		
		//Create the event
		var ev = new UserDeletedEvent(request.Id.ToString())
		{
			Oid = Guid.Empty,
			Sid = Guid.NewGuid()
		};

		//Publish the event
		await _eventBus.PublishAsync(ev, cancellationToken);

		return new DeleteUserResponse { Success = true };
    }
}
