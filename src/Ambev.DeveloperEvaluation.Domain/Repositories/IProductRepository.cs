using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations
/// </summary>
public interface IProductRepository
{
	/// <summary>
	/// Creates a new product in the repository
	/// </summary>
	/// <param name="product">The product to create</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created product</returns>
	Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a product by their unique identifier
	/// </summary>
	/// <param name="id">The unique identifier of the product</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The product if found, null otherwise</returns>
	Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes a product from the repository
	/// </summary>
	/// <param name="id">The unique identifier of the product to delete</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if the product was deleted, false if not found</returns>
	Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

	/// <summary>
	/// Get all products that matches parameters filters
	/// </summary>
	/// <param name="sortBy"></param>
	/// <param name="pageNumber"></param>
	/// <param name="pageSize"></param>
	/// <returns></returns>
	Task<List<Product>?> GetAll(string? sortBy = null, int? pageNumber = null, int? pageSize = null);

	/// <summary>
	/// Retrieves a product by their title
	/// </summary>
	/// <param name="title">The title of the product</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The product if found, null otherwise</returns>
	Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

	/// <summary>
	/// Update an existing product in the database
	/// </summary>
	/// <param name="product">The product to update</param>
	/// <returns>The updated product</returns>
	Product Update(Product product);

}
