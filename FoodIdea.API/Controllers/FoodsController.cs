
using AutoMapper;
using FoodIdea.API.Models;
using FoodIdea.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodIdea.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/foods")]
    public class FoodsController : ControllerBase
    {
        private readonly IFoodIdeaRepository _foodIdeaRepository;
        private readonly IMapper _mapper;
        public FoodsController(IFoodIdeaRepository foodIdeaRepository,IMapper mapper)
        {
            _foodIdeaRepository = foodIdeaRepository ?? throw new ArgumentNullException(nameof(foodIdeaRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<FoodWithoutRecipeDto>>> GetFoods([FromQuery] string? name, string? search,
            int pageSize = 10, int pageNumber = 1)
        {
            var foodEntities = await _foodIdeaRepository.GetFoodsAsync(name,search,pageSize,pageNumber);
            return Ok(_mapper.Map<IEnumerable<FoodWithoutRecipeDto>>(foodEntities));
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetFood(int id, bool includeRecipe = false)
        {
            var foodEntities = await _foodIdeaRepository.GetFoodAsync(id, includeRecipe);
            if(foodEntities == null)
            {
                return NotFound();
            }
            if (includeRecipe)
            {
                return Ok(_mapper.Map<FoodDto>(foodEntities));
            }

            return Ok(_mapper.Map<FoodWithoutRecipeDto>(foodEntities));
        }

    }
}
