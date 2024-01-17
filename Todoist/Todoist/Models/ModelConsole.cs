using Microsoft.EntityFrameworkCore;
using Todoist.DataAccess;
using Todoist.Entities;
using Todoist.Enum;

namespace Todoist.Model
{
    internal class ModelConsole
    {
        internal Func<string[]> GetStatuses = () => System.Enum.GetNames(typeof(StatusType));

        internal IEnumerable<Goal> SearchElementsByTitleAndDescription(List<Goal> goals, string searchWord)
        {
            return goals.FindAll(item => item.Title.Contains(searchWord) || item.Description.Contains(searchWord));
        }

        internal Goal? SearchElementByIndex(List<Goal> goals, int menuItem)
        {
            return goals[--menuItem];
        }

        internal string SearchEnumByIndex(string index)
        {
            int intIndex = Convert.ToInt32(index)-1;
            var elementOfEnum = (StatusType)intIndex;
            return Convert.ToString(elementOfEnum);
        }

        internal async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            using (var context = new ApplicationContext())
                return await context.Categories.ToListAsync();
        }

        internal async Task<IEnumerable<Goal>> GetGoalsAsync()
        {
            using (var context = new ApplicationContext())
              return await context.Goals.ToListAsync();
        }

        internal async Task AddGoalAsync(string title, string description, string status, int categoryId)
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

            //var category = Categories.Find(x => x.Id == categoryId);
            //newGoal.Category = category;
            //Goals.Add(newGoal);
        }

        internal async Task UpdateGoalAsync(Goal goalForUpdate, string titleOfGoal, string descriptionOfGoal, string categoryOfGoal, string statusOfGoal)
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

        internal async Task DeleteGoalAsync(Goal searchElementGoal)
        {
            using (var context = new ApplicationContext())
            {
                context.Goals.Remove(searchElementGoal);
                await context.SaveChangesAsync();
            }
        }
    }
}
