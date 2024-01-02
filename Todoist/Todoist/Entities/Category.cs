namespace Todoist.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string NameCategory { get; set; }
        public ICollection<Goal> Goals { get; set; }

        public override string? ToString()
        {
            string goals = "";
            foreach (var goal in Goals) 
            {
                goals += $"{goal}\n";
            }
            return $"Category - {NameCategory}\n{goals}";
        }
    }
}
