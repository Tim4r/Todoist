using Todoist.Core.Models;

namespace Todoist.Views;

internal class ViewConsole
{
    internal void Display<T>(T message)
    {
        Console.WriteLine(message);
    }

    internal Func<string> GetInput = Console.ReadLine;

    internal Func<string> GetEmpty = () => null;

    internal void OutputGoals(List<Goal> tasks)
    {
        for (int i = 0; i < tasks.Count; i++)
            Console.WriteLine($" {i + 1}.{tasks[i]}\n");
    }

    internal void OutputGoals(Task<List<Goal>> tasks)
    {
        for (int i = 0; i < tasks.Result.Count; i++)
            Console.WriteLine($" {i + 1}.{tasks.Result[i]}\n");
    }

    internal void OutputCategoryNames(List<Category> categories)
    {
        for (int i = 0; i < categories.Count; i++)
            Console.WriteLine($" {i + 1}. {categories[i].NameCategory}");
        Console.Write("\n");
    }

    internal void OutputCategories(List<Category> categories, List<Goal> goals)
    {
        for (int i = 0; i < categories.Count; i++)
            Console.WriteLine($" {i + 1}. {categories[i].ToString(goals.Where(x => x.CategoryID == i+1).ToList())}");
    }

    internal void OutputOfAvaliableStatuses(string[] statuses)
    {
        for (int i = 0; i < statuses.Length; i++)
            Console.WriteLine($" {i + 1}. {statuses[i]}");
        Console.Write("\n");
    }
}
