using AutoMapper;
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
        private readonly IFoodIdeaRepository _foodIdeaRepository;
        private readonly IMapper _mapper;

        public RecipeController(ILogger<RecipeController> logger,IMailService mailService, 
            FoodDataStore foodDataStore, IFoodIdeaRepository foodIdeaRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _foodIdeaRepository = foodIdeaRepository ?? throw new ArgumentNullException(nameof(IFoodIdeaRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
            
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> GetRecipes(int foodId)
        {
            if(!await _foodIdeaRepository.foodCheckerAsync(foodId))
            {
                _logger.LogInformation("Wybrana opcja nie istnieje");
                return NotFound();
            }
            var recipes = await _foodIdeaRepository.GetRecipesAsync(foodId);

            return Ok(_mapper.Map<IEnumerable<RecipeDto>>(recipes));
        }
        [HttpGet("{recipeid}", Name = "GetRecipe")]
        public async  Task<ActionResult<RecipeDto>> GetRecipe(int foodId, int recipeId)
        {
            if (!await _foodIdeaRepository.foodCheckerAsync(foodId))
            {
                _logger.LogInformation("Wybrana opcja nie istnieje");
                return NotFound();
            }

            var recipe = await _foodIdeaRepository.GetRecipeAsync(foodId, recipeId);
            if(recipe == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RecipeDto>(recipe));

        }
        [HttpPost]
        public async Task<ActionResult<RecipeDto>> CreateRecipe(int foodId, [FromBody] RecipeForCreationDto recipe)
        {
            if(!await _foodIdeaRepository.foodCheckerAsync(foodId))
            {
                return NotFound();
            }
            var finalRecipe = _mapper.Map<Entities.Recipe>(recipe);
            await _foodIdeaRepository.AddRecipeAsync(foodId, finalRecipe);

            await _foodIdeaRepository.SaveChangesAsync();

            var createdRecipeToReturn = _mapper.Map<Models.RecipeDto>(finalRecipe);

            return CreatedAtRoute("GetRecipe",
                new
                {
                    foodId = foodId,
                    recipeId = createdRecipeToReturn.Id
                },
                createdRecipeToReturn);
        }
        [HttpPut("{recipeid}")]
        public async Task<ActionResult> UpdateRecipe(int foodId, int recipeId, [FromBody] RecipeForUpdateDto recipe)
        {
            if(!await _foodIdeaRepository.foodCheckerAsync(foodId))
            {
                return NotFound();
            }

            var recipeEntity = await _foodIdeaRepository.GetRecipeAsync(foodId, recipeId);
            if(recipeEntity == null)
            {
                return NotFound();
            }
            var recipeFromStore = _mapper.Map<Entities.Recipe>(recipe);
            if (recipeFromStore == null)
            {
                return NotFound();
            }
            _mapper.Map(recipe, recipeEntity);
            await _foodIdeaRepository.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{recipeid}")]

        public async Task<ActionResult> RemoveRecipe(int foodId, int recipeId)
        {
            if (!await _foodIdeaRepository.foodCheckerAsync(foodId))
            {
                return NotFound();
            }
            var recipeEntity = await _foodIdeaRepository.GetRecipeAsync(foodId, recipeId);
            if (recipeEntity == null)
            {
                return NotFound();
            }
            _foodIdeaRepository.RemoveRecipe(recipeEntity);
            await _foodIdeaRepository.SaveChangesAsync();
            _mailService.Send("Recipe deleted.", $"Recipe {recipeEntity.Id} was deleted.");
            return NoContent();
        }


    }
}
