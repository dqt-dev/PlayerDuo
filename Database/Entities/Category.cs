namespace PlayerDuo.Database.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; } // game image url

        public Skill? Skill { get; set; }

    }
}