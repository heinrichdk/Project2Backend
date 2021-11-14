using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Project2Backend.Context;
using Project2Backend.Models;

namespace Project2Backend.Components;

public abstract class Repository<T> where T : DataModel
{
    protected readonly Project2Context Project2Context;

    protected Repository(Project2Context project2Context)
    {
        Project2Context = project2Context;
    }

    public async Task<string> CreateAsync(T entity)
    {
        await Project2Context.Set<T>().AddAsync(entity);
        await Project2Context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task DeleteAsync(T entity)
    {
        Project2Context.Set<T>().Remove(entity);
        await Project2Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var entity = await Project2Context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

        if (entity != null)
        {
            Project2Context.Set<T>().Remove(entity);
            await Project2Context.SaveChangesAsync();
        }
    }

    public IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
    {
        return Project2Context.Set<T>().Where(expression);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Project2Context.Set<T>().ToListAsync();
    }

    public async Task<T> GetAsync(string id)
    {
        return await Project2Context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task UpdateAsync(T entity)
    {
        Project2Context.Set<T>().Update(entity);
        await Project2Context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await Project2Context.Set<T>().CountAsync();
    }
}