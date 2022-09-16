using AutoMapper;

namespace FoodIdea.API.Profiles
{
    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<Entities.Food, Models.FoodWithoutRecipeDto>();
            CreateMap<Entities.Food, Models.FoodDto>();
        }
    }
}
    