using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;
using KontursvetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace KontursvetStore.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreDbContext _context;
    
    public ProductRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAll()
    {
        var productEntities = await _context.Products.AsNoTracking().ToListAsync();

        var products = productEntities
            .Select(p => Product.Create(
                    p.Id,
                    p.Updated,
                    p.Enabled,
                    p.Name,
                    p.Code,
                    p.Description,
                    p.ShortDescription,
                    p.Photo,
                    p.OtherPhoto.Split(";"),
                    p.Price,
                    p.Quantity
                ).Product)
            .ToList();;
        
        return products;
    }

    public async Task<Product> GetById(Guid id)
    {
        var pe = await _context.Products.FirstOrDefaultAsync(t => t.Id == id);

        return pe == null ? null : Product.Create(
            pe.Id,
            pe.Updated,
            pe.Enabled,
            pe.Name,
            pe.Code,
            pe.Description,
            pe.ShortDescription,
            pe.Photo,
            pe.OtherPhoto.Split(";"),
            pe.Price,
            pe.Quantity
            ).Product;
    }

    public async Task<Guid> Create(Product product)
    {
        var ProductEntity = new ProductEntity()
        {
            Id = product.Id,
            Name = product.Name,
            Code = product.Code,
            Description = product.Description,
            ShortDescription = product.ShortDescription,
            Photo = product.Photo,
            OtherPhoto = string.Join(";",product.OtherPhoto),
            Price = product.Price,
            Quantity = product.Quantity,
            Enabled = product.Enabled,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
        };

        var response = await _context.Products.AddAsync(ProductEntity);
        await _context.SaveChangesAsync();
        
        return response.Entity.Id;
    }

    public async Task<int> Update(Product product)
    {
        var rows = await _context.Products
            .Where(p => p.Id == product.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, product.Name)
                .SetProperty(p => p.Code, product.Code)
                .SetProperty(p => p.Description, product.Description)
                .SetProperty(p => p.ShortDescription, product.ShortDescription)
                .SetProperty(p => p.Photo, product.Photo)
                .SetProperty(p => p.OtherPhoto, string.Join(";", product.OtherPhoto))
                .SetProperty(p => p.Price, product.Price)
                .SetProperty(p => p.Quantity, product.Quantity)
                .SetProperty(p => p.Enabled, p => product.Enabled)
                .SetProperty(p => p.Updated, p => DateTime.UtcNow)
            );
        
        return rows;
    }

    public async Task<int> Delete(Guid id)
    {
        var rows = await _context.Products
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        
        return rows;
    }
}