namespace LearnDapper.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Status { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
