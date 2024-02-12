using Todoist.Core.Models;
using Todoist.Data.Context;
using Microsoft.EntityFrameworkCore;
using Todoist.Core.Enums;

namespace Todoist.BL;

public class BusinessLogic
{
    public async Task CreateGoalAsync(string title, string description, string status, int categoryId)
    {
        var newGoal = new Goal()
        {
            Title = title,
            Description = description,
            Created = DateTime.UtcNow,
            Status = status,
            CategoryID = categoryId,
        };

        using (var context = new ApplicationContext())
        {
            await context.Goals.AddAsync(newGoal);
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateGoalAsync(Goal goalForUpdate, string titleOfGoal, string descriptionOfGoal, string categoryOfGoal, string statusOfGoal)
    {
        using (var context = new ApplicationContext())
        {
            var newGoal = await context.Goals.Where(x => x.Id == goalForUpdate.Id).FirstOrDefaultAsync();
            if (titleOfGoal != null)
                newGoal.Title = titleOfGoal;
            if (descriptionOfGoal != null)
                newGoal.Description = descriptionOfGoal;
            if (categoryOfGoal != null)
                newGoal.CategoryID = Convert.ToInt32(categoryOfGoal);
            if (statusOfGoal != null)
                newGoal.Status = statusOfGoal;

            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Goal>> GetGoalsAsync()
    {
        using (var context = new ApplicationContext())
            return await context.Goals.ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        using (var context = new ApplicationContext())
            return await context.Categories.ToListAsync();
    }

    public async Task DeleteGoalAsync(Goal searchElementGoal)
    {
        using (var context = new ApplicationContext())
        {
            context.Goals.Remove(searchElementGoal);
            await context.SaveChangesAsync();
        }
    }


    public Goal? SearchGoal(List<Goal> goals, int menuItem)
    {
        return goals[--menuItem];
    }

    public IEnumerable<Goal> SearchByTitleDescription(List<Goal> goals, string searchWord)
    {
        return goals.FindAll(item => item.Title.Contains(searchWord) || item.Description.Contains(searchWord));
    }

    public Func<string[]> GetStatuses = () => Enum.GetNames(typeof(StatusType));

    public string GetStatus(string index) => Enum.GetName(typeof(StatusType), int.Parse(index));
}