using FoodIdea.API.Entities;
using System.ComponentModel.DataAnnotations;

namespace FoodIdea.API.Models
{
    public class FoodDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<RecipeDto> Recipe { get; set; } = new List<RecipeDto>();

    }
}
