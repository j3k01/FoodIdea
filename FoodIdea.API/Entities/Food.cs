using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodIdea.API.Entities
{
    public class Food
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Recipe> Recipe { get; set; } = new List<Recipe>();

        public Food(string name)
        {
            Name = name;
        }
    }
}
