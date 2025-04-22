using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUser;

/// <summary>
/// Handler for processing ListUserCommand requests
/// </summary>
public class ListUserHandler : IRequestHandler<ListUserCommand, ListUserResult>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initializes a new instance of ListUserHandler
	/// </summary>
	/// <param name="userRepository">The user repository</param>
	/// <param name="mapper">The AutoMapper instance</param>
	/// <param name="validator">The validator for ListUserCommand</param>
	public ListUserHandler(
		IUserRepository userRepository,
		IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	/// <summary>
	/// Handles the ListUserCommand request
	/// </summary>
	/// <param name="request">The ListUser command</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The user list if found</returns>
	public async Task<ListUserResult> Handle(ListUserCommand request, CancellationToken cancellationToken)
	{
		var validator = new ListUserValidator();
		var validationResult = await validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
			throw new ValidationException(validationResult.Errors);

		var users = await _userRepository.GetAll(request.SortOrder, request.PageNumber, request.PageSize);

		if (users == null || !users.Any())
			throw new KeyNotFoundException($"Users were not found");

		return new ListUserResult { Users = users };
	}
}
