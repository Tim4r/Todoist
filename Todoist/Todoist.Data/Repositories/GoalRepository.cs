using Microsoft.EntityFrameworkCore;
using Todoist.Core.Interfaces;
using Todoist.Core.Models;
using Todoist.Data.Context;

namespace Todoist.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly ApplicationContext _context;

    public GoalRepository(ApplicationContext context) => _context = context;

    public async Task CreateAsync(Goal goal)
    {
        await _context.Goals.AddAsync(goal);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Goal goalForUpdate, string titleOfGoal, string descriptionOfGoal, string categoryOfGoal, string statusOfGoal)
    {
        var newGoal = await _context.Goals.Where(x => x.Id == goalForUpdate.Id).FirstOrDefaultAsync();
        if (titleOfGoal != null)
            newGoal.Title = titleOfGoal;
        if (descriptionOfGoal != null)
            newGoal.Description = descriptionOfGoal;
        if (categoryOfGoal != null)
            newGoal.CategoryID = Convert.ToInt32(categoryOfGoal);
        if (statusOfGoal != null)
            newGoal.Status = statusOfGoal;
    await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _context.Categories.ToListAsync();

    public async Task<IEnumerable<Goal>> GetGoalsAsync() => await _context.Goals.ToListAsync();

    public async Task DeleteAsync(Goal goal)
    {
        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
