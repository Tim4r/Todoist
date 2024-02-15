using Todoist.Core.Models;

namespace Todoist.Core.Interfaces;

public interface IGoalRepository
{
    public Task CreateAsync(Goal goal);
    public Task UpdateAsync(Goal goalForUpdate, string titleOfGoal, string descriptionOfGoal, string categoryOfGoal, string statusOfGoal);
    public Task<IEnumerable<Category>> GetCategoriesAsync();
    public Task<IEnumerable<Goal>> GetGoalsAsync();
    public Task DeleteAsync(Goal goal);
}
