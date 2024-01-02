namespace Todoist.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime Created { get; init; }
        public required string Status { get; set; }

        public required int CategoryID { get; set; }
        public Category Category { get; set; } = null!; //Navigation property

        public override string? ToString()
        {
            return $" Title - {Title}, Description - {Description}, Date of creation - {Created}, Status - {Status};";
        }
    }
}
