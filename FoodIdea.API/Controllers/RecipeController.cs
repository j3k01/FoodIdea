using FoodIdea.API.Models;
using FoodIdea.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodIdea.API.Controllers
{
    [Route("api/foods/{foodId}/recipe")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IMailService _mailService;
        private readonly FoodDataStore _foodDataStore;
        public RecipeController(ILogger<RecipeController> logger,IMailService mailService, FoodDataStore foodDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _foodDataStore = foodDataStore ?? throw new ArgumentNullException(nameof(foodDataStore));
        }
        [HttpGet]
        public ActionResult<IEnumerable<RecipeDto>> GetRecipes(int foodId)
        {
            var food = _foodDataStore.Foods.FirstOrDefault(x => x.Id == foodId);
            if (food == null)
            {
                _logger.LogInformation("Podany posiłek nie istnieje.");
                return NotFound();
            }
            return Ok(food.Recipe);
        }
        [HttpGet("{recipeid}", Name = "GetRecipe")]
        public ActionResult<RecipeDto> GetRecipe(int foodId, int recipeId)
        {
            var food = _foodDataStore.Foods.FirstOrDefault(x => x.Id == foodId);
            if(food == null)
            {
                _logger.LogInformation("Podany posiłek nie istnieje.");
                return NotFound();
            }
            var recipe = food.Recipe.FirstOrDefault(x => x.Id == recipeId);
            if(recipe == null)
            {
                _logger.LogInformation("Podany przepis nie istnieje.");
                return NotFound();
            }

            return Ok(recipe);
        }
        [HttpPost]
        public ActionResult<RecipeDto> CreateRecipe(int foodId, [FromBody] RecipeForCreationDto recipe)
        {
            var food = _foodDataStore.Foods.FirstOrDefault(x => x.Id == foodId);
            if (food == null)
            {
                return NotFound();
            }
            var maxRecipeId = _foodDataStore.Foods.SelectMany(x => x.Recipe).Max(c => c.Id);
            var finalRecipe = new RecipeDto()
            {
                Id = ++maxRecipeId,
                Ingredients = recipe.Ingredients,
                PreparationTime = recipe.PreparationTime,
                IsHardToDo = recipe.IsHardToDo,
                TasteRate = recipe.TasteRate
            };
            food.Recipe.Add(finalRecipe);
            return CreatedAtRoute("GetRecipe",
                new
                {
                    foodId = foodId,
                    recipeId = finalRecipe.Id
                },
                finalRecipe);
        }
        [HttpPut("{recipeid}")]
        public ActionResult UpdateRecipe(int foodId, int recipeId ,[FromBody] RecipeForUpdateDto recipe)
        {
            var food = _foodDataStore.Foods.FirstOrDefault(x => x.Id == foodId);
            if (food == null)
            {
                return NotFound();
            }
            var recipeFromStore = food.Recipe.FirstOrDefault(x => x.Id == recipeId);
            if (recipeFromStore == null)
            {
                return NotFound();
            }
            recipeFromStore.Ingredients = recipe.Ingredients;
            recipeFromStore.IsHardToDo = recipe.IsHardToDo;
            recipeFromStore.PreparationTime = recipe.PreparationTime;
            recipeFromStore.TasteRate = recipe.TasteRate;

            return NoContent();
        }
        [HttpDelete("{recipeid}")]
        
        public ActionResult RemoveRecipe(int foodId,int recipeId)
        {
            var food = _foodDataStore.Foods.FirstOrDefault(x => x.Id == foodId);
            if (food == null)
            {
                return NotFound();
            }
            var recipeFromStore = food.Recipe.FirstOrDefault(x => x.Id == recipeId);
            if (recipeFromStore == null)
            {
                return NotFound();
            }
            food.Recipe.Remove(recipeFromStore);
            _mailService.Send("Recipe deleted.", $"Recipe {recipeFromStore.Id} was deleted.");
            return NoContent();
        }

        
    }
}
