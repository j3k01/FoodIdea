
using FoodIdea.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodIdea.API.Controllers
{
    [ApiController]
    [Route("api/foods")]
    public class FoodsController : ControllerBase
    {
        private readonly FoodDataStore _foodDataStore;
        public FoodsController(FoodDataStore foodDataStore)
        {
            _foodDataStore = foodDataStore ?? throw new ArgumentNullException(nameof(foodDataStore));
        }
        [HttpGet]
        public ActionResult<IEnumerable<FoodDto>> GetFoods()
        {
            return Ok(_foodDataStore.Foods);
        }

        [HttpGet("{id}")]
        
        public ActionResult<FoodDto> GetFood(int id)
        {
            var foodToReturn = _foodDataStore.Foods
                .FirstOrDefault(x => x.Id == id);

            if(foodToReturn == null)
            {
                return NotFound();
            }
            return Ok(foodToReturn);
        }

    }
}
