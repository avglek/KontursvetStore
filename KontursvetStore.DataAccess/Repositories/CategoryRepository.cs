using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;
using KontursvetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace KontursvetStore.DataAccess.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly StoreDbContext _context;
    
    public CategoryRepository(StoreDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAll()
    {
        var categoryEntities = await _context.Categories.AsNoTracking().ToListAsync();

        var categories = categoryEntities
            .Select(p => Category.Create(p.Id, p.Name, p.Description, p.Enabled).Category)
            .ToList();;
        
        return categories;
    }

    public async Task<Category> GetById(Guid id)
    {
        var ce = await _context.Categories.SingleOrDefaultAsync(t => t.Id == id);

        return Category.Create(ce.Id, ce.Name, ce.Description, ce.Enabled).Category;
    }

    public async Task<Guid> Create(Category category)
    {
        var categoryEntity = new CategoryEntity()
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Enabled = category.Enabled,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
        };

        await _context.Categories.AddAsync(categoryEntity);
        await _context.SaveChangesAsync();
        
        return categoryEntity.Id;
    }

    public async Task<Guid> Update(Category category)
    {
        await _context.Categories
            .Where(p => p.Id == category.Id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Name, b => category.Name)
                .SetProperty(p => p.Description, p => category.Description)
                .SetProperty(p => p.Enabled, p => category.Enabled)
                .SetProperty(p => p.Updated, p => DateTime.UtcNow)
            );
        
        return category.Id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await _context.Categories
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}