namespace FoodIdea.API.Models
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public List<string> Ingredients { get; set; } = new List<string>();

        public int PreparationTime { get; set; }
        public bool IsHardToDo { get; set; }
        public double TasteRate { get; set; }
        public int NumberOfIngredients
        {
            get { return Ingredients.Count; }
        }
    }
}
