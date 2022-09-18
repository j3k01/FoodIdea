using FoodIdea.API.Entities;

namespace FoodIdea.API.Services
{
    public interface IFoodIdeaRepository
    {
        Task<IEnumerable<Food>> GetFoodsAsync();
        Task<IEnumerable<Food>> GetFoodsAsync(string? name, string? search, int pageSize, int pageNumber);

        Task<Food?> GetFoodAsync(int foodId, bool includeRecipe);
        Task<bool> foodCheckerAsync(int foodId);
        Task<IEnumerable<Recipe>> GetRecipesAsync(int foodId);
        Task<Recipe?> GetRecipeAsync(int foodId,int recipeId);
        Task AddRecipeAsync(int foodId, Recipe recipe);
        void RemoveRecipe(Recipe recipe);
        Task<bool> SaveChangesAsync();
    }
}
