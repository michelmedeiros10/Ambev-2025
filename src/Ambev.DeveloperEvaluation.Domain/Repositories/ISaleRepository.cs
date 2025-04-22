using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entity operations
/// </summary>
public interface ISaleRepository
{
	/// <summary>
	/// Creates a new sale in the repository
	/// </summary>
	/// <param name="sale">The sale to create</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created sale</returns>
	Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a sale by their unique identifier
	/// </summary>
	/// <param name="id">The unique identifier of the sale</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The sale if found, null otherwise</returns>
	Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes a sale from the repository
	/// </summary>
	/// <param name="id">The unique identifier of the sale to delete</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if the sale was deleted, false if not found</returns>
	Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Get all sales that matches parameters filters
	/// </summary>
	/// <param name="sortBy"></param>
	/// <param name="pageNumber"></param>
	/// <param name="pageSize"></param>
	/// <returns></returns>
	Task<List<Sale>?> GetAll(string? sortBy = null, int? pageNumber = null, int? pageSize = null);

	/// <summary>
	/// Retrieves a sale by their sale number
	/// </summary>
	/// <param name="saleNumber">The number of the sale</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The sale if found, null otherwise</returns>
	Task<Sale?> GetBySaleNumberAsync(int saleNumber, CancellationToken cancellationToken = default);
}
