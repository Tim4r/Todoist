namespace Todoist.Entities;

public class Category
{
    public int Id { get; set; }
    public required string NameCategory { get; set; }

    public string? ToString(List<Goal> goals)
    {
        string newgoals = "";
        foreach (var goal in goals) 
        {
            newgoals += $"{goal}\n";
        }
        return $"Category - {NameCategory}\n{newgoals}";
    }
}
