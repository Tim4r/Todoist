using Todoist.Core.Interfaces;
using Todoist.Data.Context;

namespace Todoist.Repositories;

class GoalRepository : IGoalRepository
{
    private readonly ApplicationContext _context;

    public GoalRepository()
    {
        _context = new ApplicationContext();
    }

    public GoalRepository(ApplicationContext context)
    {
        _context = context;
    }

    //internal Task<IEnumerable<Goal>> InsertAsync(Goal goal)
    //{
        
    //}

    //internal Task<IEnumerable<Goal>> UpdateAsync(Goal goal)
    //{

    //}

    //internal Task<IEnumerable<Category>> GetCategoriesAsync()
    //{

    //}

    //internal Task<IEnumerable<Goal>> GetGoalsAsync()
    //{

    //}

    //internal Task DeleteGoalAsync(Goal goal)
    //{

    //}


    public void Save()
    {
        _context.SaveChanges();
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
