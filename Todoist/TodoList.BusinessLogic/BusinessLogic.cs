using Todoist.Core.Enums;
using Todoist.Core.Interfaces;
using Todoist.Core.Models;
using Todoist.Data.Context;
using Todoist.Repositories;

namespace Todoist.BL;

public class BusinessLogic
{
    IGoalRepository _goalRepository = new GoalRepository(new ApplicationContext());

    public async Task CreateAsync(string title, string description, string status, int categoryId)
    {
        var newGoal = new Goal()
        {
            Title = title,
            Description = description,
            Created = DateTime.UtcNow,
            Status = status,
            CategoryID = categoryId,
        };

        await _goalRepository.CreateAsync(newGoal);
    }

    public async Task UpdateAsync(Goal goalForUpdate, string titleOfGoal, string descriptionOfGoal, string categoryOfGoal, string statusOfGoal)
    {
        await _goalRepository.UpdateAsync(goalForUpdate, titleOfGoal, descriptionOfGoal, categoryOfGoal, statusOfGoal);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _goalRepository.GetCategoriesAsync();

    public async Task<IEnumerable<Goal>> GetGoalsAsync() => await _goalRepository.GetGoalsAsync();

    public async Task DeleteAsync(Goal searchElementGoal) => await _goalRepository.DeleteAsync(searchElementGoal);


    public Goal? SearchGoal(List<Goal> goals, int menuItem) => goals[--menuItem];

    public IEnumerable<Goal> SearchByTitleDescription(List<Goal> goals, string searchWord)
    {
        return goals.FindAll(item => item.Title.Contains(searchWord) || item.Description.Contains(searchWord));
    }

    public Func<string[]> GetStatuses = () => Enum.GetNames(typeof(StatusType));

    public string GetStatus(string index) => Enum.GetName(typeof(StatusType), int.Parse(index));
}