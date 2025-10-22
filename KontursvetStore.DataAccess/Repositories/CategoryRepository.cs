using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;
using KontursvetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KontursvetStore.DataAccess.Repositories;

public class CategoryRepository(StoreDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAll()
    {
        var categoryEntities = await context.Categories
            .Include(c => c.Products)
            .ToListAsync();
        
        var categories = categoryEntities.Select(ce => Category.Create(
            id: ce.Id,
            lastUpdated: (DateTime)ce.Updated,
            name: ce.Name,
            description: ce.Description,
            imageUrl: ce.ImageUrl,
            enabled: ce.Enabled,
            products: ce.Products.Select( pe => Product.Create(
                id: pe.Id,
                categoryId: pe.CategoryId,
                lastUpdate: pe.Updated,
                enabled: pe.Enabled,
                name: pe.Name,
                code: pe.Code,
                description: pe.Description,
                shortDescription: pe.ShortDescription,
                photo: pe.Photo,
                otherPhoto: pe.OtherPhoto.Split(";"),
                price: pe.Price,
                quantity: pe.Quantity,
                orders:  [],
                category: null
                ).Value).ToList()
            ).Value).ToList();
        
        return categories;
        return null;
    }

    public async Task<Category> GetById(Guid id)
    {
        var ce = await context.Categories
            .Include( c => c.Products )
            .FirstOrDefaultAsync(t => t.Id == id);

        return ce == null
            ? null
            : Category.Create(
                ce.Id,
                ce.Updated,
                ce.Enabled,
                ce.Name,
                ce.Description,
                ce.ImageUrl,
                products: ce.Products.Select(pe => Product.Create(
                    id: pe.Id,
                    categoryId: pe.CategoryId,
                    lastUpdate: pe.Updated,
                    enabled: pe.Enabled,
                    name: pe.Name,
                    code: pe.Code,
                    description: pe.Description,
                    shortDescription: pe.ShortDescription,
                    photo: pe.Photo,
                    otherPhoto: pe.OtherPhoto.Split(";"),
                    price: pe.Price,
                    quantity: pe.Quantity,
                    orders: [],
                    category: null
                ).Value).ToList()
            ).Value;
    }

    public async Task<Guid> Create(Category category)
    {
        var categoryEntity = new CategoryEntity()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ImageUrl = category.ImageUrl,
            Enabled = category.Enabled,
            Created = category.LastUpdated,
            Updated = category.LastUpdated,
        };

        var response = await context.Categories.AddAsync(categoryEntity);
        await context.SaveChangesAsync();
        
        return response.Entity.Id;
    }

    public async Task<int> Update(Category category)
    {
        var rows = await context.Categories
            .Where(p => p.Id == category.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, b => category.Name)
                .SetProperty(p => p.Description, p => category.Description)
                .SetProperty(p => p.ImageUrl, b => category.ImageUrl)
                .SetProperty(p => p.Enabled, p => category.Enabled)
                .SetProperty(p => p.Updated, p => DateTime.UtcNow)
                // .SetProperty(p => p.Products, p => category.Products
                //     .Select( t => new ProductEntity()
                //         {
                //             Id = t.Id,
                //             Updated = t.LastUpdated,
                //             Name = t.Name,
                //             CategoryId = t.CategoryId,
                //             Description = t.Description,
                //             Enabled = t.Enabled,
                //             Price = t.Price,
                //             Quantity = t.Quantity,
                //             Code = t.Code    
                //         }) 
                //)
            );
        
        return rows;
    }
    
    public async Task<int> Delete(Guid id)
    {
        var rows = await context.Categories
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        
        return rows;
    }
}