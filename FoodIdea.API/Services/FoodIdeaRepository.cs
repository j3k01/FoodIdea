using FoodIdea.API.DbContexts;
using FoodIdea.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodIdea.API.Services
{
    public class FoodIdeaRepository : IFoodIdeaRepository
    {
        private readonly FoodIdeaContext _context;
        public FoodIdeaRepository(FoodIdeaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Food>> GetFoodsAsync()
        {
            return await _context.Foods.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Food?> GetFoodAsync(int foodId, bool includeRecipe)
        {
            if(includeRecipe)
            {
                return await _context.Foods.Include(c => c.Recipe).Where(c => c.Id == foodId).FirstOrDefaultAsync();
            }

            return await _context.Foods.Where(c => c.Id == foodId).FirstOrDefaultAsync();

        }

        public async Task<bool> foodCheckerAsync(int foodId)
        {
            return await _context.Foods.AnyAsync(c => c.Id == foodId);
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync(int foodId)
        {
            return await _context.Recipes.Where(p => p.FoodId == foodId).ToListAsync();

        }

        public async Task<Recipe?> GetRecipeAsync(int foodId, int recipeId)
        {
            return await _context.Recipes.Where(p => p.FoodId == foodId && p.Id == recipeId).FirstOrDefaultAsync();
        }

        public async Task AddRecipeAsync(int foodId, Recipe recipe)
        {
            var food = await GetFoodAsync(foodId, false);
            if(food != null)
            {
                food.Recipe.Add(recipe);
            }
        }

        public void RemoveRecipe(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
        }

       public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
