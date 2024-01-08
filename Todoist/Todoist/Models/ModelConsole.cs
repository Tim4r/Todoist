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

        internal List<Category> GetCategories()
        {
            List<Category> categories;
            using (var context = new ApplicationContext())
            {
                 categories = new List<Category>();
                categories = context.Categories.ToList();
            }
            return categories;
        }

        internal List<Goal> GetGoals()
        {
            List<Goal> goals;
            using (var context = new ApplicationContext())
            {
                goals = new List<Goal>();
                goals = context.Goals.ToList();
            }
            return goals;
        }

        internal void Add(string title, string description, string status, int categoryId)
        {
            //var category = Categories.Find(x => x.Id == categoryId);
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
                context.Goals.Add(newGoal);
                context.SaveChanges();
            }

            //newGoal.Category = category;
            //Goals.Add(newGoal);
        }

        internal void Update(List<Goal> goals, Goal goalForUpdate, List<string> newProperties, int menuItem)
        {
            int indexGoal = --menuItem;
            if (newProperties[0] != null)
                goalForUpdate.Title = newProperties[0];
            if (newProperties[1] != null)
                goalForUpdate.Description = newProperties[1];
            if (newProperties[2] != null)
                goalForUpdate.CategoryID = Convert.ToInt32(newProperties[2]);
            if (newProperties[3] != null)
                goalForUpdate.Status = newProperties[3];

            goals[indexGoal] = goalForUpdate;
                
            using (var context = new ApplicationContext())
                context.SaveChanges();
        }

        internal void Delete(Goal searchElementGoal)
        {
            using (var context = new ApplicationContext())
            {
                context.Goals.Remove(searchElementGoal);
                context.SaveChanges();
            }
        }
    }
}
