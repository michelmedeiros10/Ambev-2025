using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IUserRepository using Entity Framework Core
/// </summary>
public class UserRepository : IUserRepository
{
	private readonly DefaultContext _context;

	/// <summary>
	/// Initializes a new instance of UserRepository
	/// </summary>
	/// <param name="context">The database context</param>
	public UserRepository(DefaultContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Creates a new user in the database
	/// </summary>
	/// <param name="user">The user to create</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created user</returns>
	public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
	{
		await _context.Users.AddAsync(user, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return user;
	}

	/// <summary>
	/// Retrieves a user by their unique identifier
	/// </summary>
	/// <param name="id">The unique identifier of the user</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The user if found, null otherwise</returns>
	public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Users
			.Include(u => u.Name).Include(u => u.Address)
			.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
	}

	/// <summary>
	/// Retrieves a user by their email address
	/// </summary>
	/// <param name="email">The email address to search for</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The user if found, null otherwise</returns>
	public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		return await _context.Users.AsNoTracking()
			.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
	}

	/// <summary>
	/// Deletes a user from the database
	/// </summary>
	/// <param name="id">The unique identifier of the user to delete</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if the user was deleted, false if not found</returns>
	public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var user = await GetByIdAsync(id, cancellationToken);

		if (user == null)
			return false;

		if (user.Name != null)
			_context.PersonNames.Remove(user.Name);

		if (user.Address != null)
			_context.PersonAddress.Remove(user.Address);

		_context.Users.Remove(user);

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	/// <summary>
	/// Get all users that matches filters parameters
	/// </summary>
	/// <param name="sortBy">Sorting parameters</param>
	/// <param name="pageNumber">Page number</param>
	/// <param name="pageSize">Page size</param>
	/// <returns></returns>
	public async Task<List<User>?> GetAll(string? sortBy = null, int? pageNumber = null, int? pageSize = null)
	{
		if (string.IsNullOrWhiteSpace(sortBy))
		{
			sortBy = "Username";
		}

		if (pageNumber == null || pageNumber <= 0)
		{
			pageNumber = 1;
		}

		if (pageSize == null || pageSize <= 0) 
		{
			pageSize = 10;
		};

		var orderedUsers = _context.Users.AsNoTracking().AsQueryable().OrderBy(sortBy).ToList();

		var users = orderedUsers
							.Skip(((int)pageNumber - 1) * (int)pageSize)
							.Take((int)pageSize)
							.ToList();
		return users;
	}

	/// <summary>
	/// Update an existing user in the database
	/// </summary>
	/// <param name="user">The user to update</param>
	/// <returns>The updated user</returns>
	public User Update(User user)
	{
		_context.Users.Update(user);
		_context.SaveChanges();

		return user;
	}
}
