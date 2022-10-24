namespace PlayerDuo.Database.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; } // game image url

        public List<Skill>? Skills { get; set; }

    }
}