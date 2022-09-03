using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodIdea.API.Entities
{
    public class Recipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [NotMapped]
        public List<string> IngredientsList { get; set; } = new List<string>();
        public int PreparationTime { get; set; }
        public bool IsHardToDo { get; set; }
        public double TasteRate { get; set; }        
        public string HowToMake { get; set; } = string.Empty;

        [ForeignKey("FoodId")]
        public Food? Food { get; set; }

        public int FoodId { get; set; }
    }
}
