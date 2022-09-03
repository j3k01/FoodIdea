using System.ComponentModel.DataAnnotations;

namespace FoodIdea.API.Models
{
    public class RecipeForCreationDto
    {
        [Required(ErrorMessage ="Ingredients are required!")]
        [MaxLength(50)]
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
