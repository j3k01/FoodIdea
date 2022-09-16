namespace FoodIdea.API.Models
{
    public class FoodWithoutRecipeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
