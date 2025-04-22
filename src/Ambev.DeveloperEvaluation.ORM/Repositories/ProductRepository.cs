using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
	private readonly DefaultContext _context;

	/// <summary>
	/// Initializes a new instance of ProductRepository
	/// </summary>
	/// <param name="context">The database context</param>
	public ProductRepository(DefaultContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Creates a new product in the database
	/// </summary>
	/// <param name="product">The product to create</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created product</returns>
	public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
	{
		await _context.Products.AddAsync(product, cancellationToken);
		await _context.SaveChangesAsync(cancellationToken);
		return product;
	}

	/// <summary>
	/// Retrieves a product by their unique identifier
	/// </summary>
	/// <param name="id">The unique identifier of the product</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The product if found, null otherwise</returns>
	public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Products
			.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
	}

	/// <summary>
	/// Deletes a product from the database
	/// </summary>
	/// <param name="id">The unique identifier of the product to delete</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if the product was deleted, false if not found</returns>
	public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var product = await GetByIdAsync(id, cancellationToken);

		if (product == null)
			return false;

		_context.Products.Remove(product);

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	/// <summary>
	/// Get all products that matches filters parameters
	/// </summary>
	/// <param name="sortBy">Sorting parameters</param>
	/// <param name="pageNumber">Page number</param>
	/// <param name="pageSize">Page size</param>
	/// <returns></returns>
	public async Task<List<Product>?> GetAll(string? sortBy = null, int? pageNumber = null, int? pageSize = null)
	{
		if (string.IsNullOrWhiteSpace(sortBy))
		{
			sortBy = "Title";
		}

		if (pageNumber == null || pageNumber <= 0)
		{
			pageNumber = 1;
		}

		if (pageSize == null || pageSize <= 0)
		{
			pageSize = 10;
		};

		var orderedProducts = _context.Products.AsNoTracking().AsQueryable().OrderBy(sortBy).ToList();

		var products = orderedProducts
							.Skip(((int)pageNumber - 1) * (int)pageSize)
							.Take((int)pageSize)
							.ToList();
		return products;
	}

	/// <summary>
	/// Retrieves a product by their title
	/// </summary>
	/// <param name="title">The title of the product</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The product if found, null otherwise</returns>
	public async Task<Product?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
	{
		return await _context.Products.AsNoTracking()
			.FirstOrDefaultAsync(o => o.Title == title, cancellationToken);
	}

	/// <summary>
	/// Update an existing product in the database
	/// </summary>
	/// <param name="product">The product to update</param>
	/// <returns>The updated product</returns>
	public Product Update(Product product)
	{
		var prd = _context.Products.First(p => p.Id == product.Id);
		prd.Category = product.Category;
		prd.Count = product.Count;
		prd.Description = product.Description;
		prd.Image = product.Image;
		prd.Price = product.Price;
		prd.Rate = product.Rate;
		prd.Title = product.Title;

		_context.Products.Update(prd);
		_context.SaveChanges();

		return prd;
	}
}
