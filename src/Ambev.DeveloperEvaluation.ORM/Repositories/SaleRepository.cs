using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ISaleRepository using Entity Framework Core
/// </summary>
public class SaleRepository : ISaleRepository
{
	private readonly DefaultContext _context;

	/// <summary>
	/// Initializes a new instance of SaleRepository
	/// </summary>
	/// <param name="context">The database context</param>
	public SaleRepository(DefaultContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Creates a new sale in the database
	/// </summary>
	/// <param name="sale">The sale to create</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The created sale</returns>
	public async Task<Sale> CreateAsync(Sale sale, CancellationToken cancellationToken = default)
	{
		var products = sale.Products;
		sale.Products = null;

		await _context.Sales.AddAsync(sale, cancellationToken);

		if (products != null && products.Any())
		{
			foreach (var saleProduct in products)
			{				
				saleProduct.Sale = sale;
				saleProduct.Product = await _context.Products.FirstOrDefaultAsync(p => p.Id.ToString() == saleProduct.ProductRefId);
				if (saleProduct.Product == null)
					throw new KeyNotFoundException($"Product with Id=>[{saleProduct.ProductRefId}] was not found");
				
				//Update the product quantity
				saleProduct.Product.Count -= (int)saleProduct.Quantity;
				_context.Products.Update(saleProduct.Product);

				await _context.SaleProduct.AddAsync(saleProduct, cancellationToken);
			}
		}

		await _context.SaveChangesAsync(cancellationToken);
		return sale;
	}

	/// <summary>
	/// Retrieves a sale by their unique identifier
	/// </summary>
	/// <param name="id">The unique identifier of the sale</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The sale if found, null otherwise</returns>
	public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		bool exist = await _context.Sales.AsNoTracking().AnyAsync(x => x.Id == id);

		if (exist)
		{
			return await _context.Sales.AsNoTracking()
				.Include(s => s.Products)
				.ThenInclude(p => p.Product)
				.AsNoTracking()				
				.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
		}
		else
		{
			return default(Sale);
		}
	}

	/// <summary>
	/// Deletes a sale from the database
	/// </summary>
	/// <param name="id">The unique identifier of the sale to delete</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if the sale was deleted, false if not found</returns>
	public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var sale = await GetByIdAsync(id, cancellationToken);

		if (sale == null)
			return false;

		_context.Sales.Remove(sale);

		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	/// <summary>
	/// Get all sales that matches filters parameters
	/// </summary>
	/// <param name="sortBy">Sorting parameters</param>
	/// <param name="pageNumber">Page number</param>
	/// <param name="pageSize">Page size</param>
	/// <returns></returns>
	public async Task<List<Sale>?> GetAll(string? sortBy = null, int? pageNumber = null, int? pageSize = null)
	{
		if (string.IsNullOrWhiteSpace(sortBy))
		{
			sortBy = "SaleNumber";
		}

		if (pageNumber == null || pageNumber <= 0)
		{
			pageNumber = 1;
		}

		if (pageSize == null || pageSize <= 0)
		{
			pageSize = 10;
		};

		var orderedSales = _context.Sales.AsNoTracking().AsQueryable().OrderBy(sortBy).ToList();

		var sales = orderedSales
							.Skip(((int)pageNumber - 1) * (int)pageSize)
							.Take((int)pageSize)
							.ToList();
		return sales;
	}

	/// <summary>
	/// Retrieves a sale by their sale number
	/// </summary>
	/// <param name="saleNumber">The number of the sale</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>The sale if found, null otherwise</returns>
	public async Task<Sale?> GetBySaleNumberAsync(int saleNumber, CancellationToken cancellationToken = default)
	{
		bool exist = await _context.Sales.AsNoTracking().AnyAsync(x => x.SaleNumber == saleNumber);

		if (exist)
		{
			return await _context.Sales.AsNoTracking()
				.Include(s => s.Products)
				.ThenInclude(p => p.Product)
				.AsNoTracking()
				.FirstOrDefaultAsync(o => o.SaleNumber == saleNumber, cancellationToken);
		}
		else
		{
			return default(Sale);
		}
	}
}
