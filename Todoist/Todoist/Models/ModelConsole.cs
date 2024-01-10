using Microsoft.EntityFrameworkCore;
using Todoist.DataAccess;
using Todoist.Entities;
using Todoist.Enum;

namespace Todoist.Model
{
    internal class ModelConsole
    {
        internal Func<string[]> GetStatuses = () => System.Enum.GetNames(typeof(StatusType));

        internal List<Goal> SearchElementsByTitleAndDescription(List<Goal> goals, string searchWord)
        {
            return goals.FindAll(item => item.Title.Contains(searchWord) || item.Description.Contains(searchWord));
        }

        internal Goal? SearchElementByIndex(Task<List<Goal>> goals, int menuItem)
        {
            return goals.Result[--menuItem];
        }

        internal string SearchEnumByIndex(string index)
        {
            int intIndex = Convert.ToInt32(index)-1;
            var elementOfEnum = (StatusType)intIndex;
            return Convert.ToString(elementOfEnum);
        }

        internal async Task<List<Category>> GetCategoriesAsync()
        {
            using (var context = new ApplicationContext())
                return await context.Categories.ToListAsync();
        }

        internal async Task<List<Goal>> GetGoalsAsync()
        {
            using (var context = new ApplicationContext())
              return await context.Goals.ToListAsync();
        }

        internal async Task AddAsync(string title, string description, string status, int categoryId)
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

        internal async Task UpdateAsync(Goal goalForUpdate, List<string> newProperties)
        {
            using (var context = new ApplicationContext())
            {
                var newGoal = await context.Goals.Where(x => x.Id == goalForUpdate.Id).FirstOrDefaultAsync();
                if (newProperties[0] != null)
                    newGoal.Title = newProperties[0];
                if (newProperties[1] != null)
                    newGoal.Description = newProperties[1];
                if (newProperties[2] != null)
                    newGoal.CategoryID = Convert.ToInt32(newProperties[2]);
                if (newProperties[3] != null)
                    newGoal.Status = newProperties[3];

                await context.SaveChangesAsync();
            }
        }

        internal async Task DeleteAsync(Goal searchElementGoal)
        {
            using (var context = new ApplicationContext())
            {
                context.Goals.Remove(searchElementGoal);
                await context.SaveChangesAsync();
            }
        }
    }
}
