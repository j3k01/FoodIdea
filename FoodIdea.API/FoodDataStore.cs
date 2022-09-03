using FoodIdea.API.Models;

namespace FoodIdea.API
{
    public class FoodDataStore
    {
        public List<FoodDto> Foods { get; set; }

        public FoodDataStore()
        {
            Foods = new List<FoodDto>()
            {
                new FoodDto()
                {
                    Id = 1,
                    Name = "Spaghetti",
                    Description = "Włoskie danie z makaronem i mięsem mielonym",
                    Recipe = new List<RecipeDto>()
                    {
                        new RecipeDto()
                        {
                            Id = 1,
                            PreparationTime = 30,
                            Ingredients = {
                                "makaron","mięso mielone","ser","sos","sól","olej"
                            },
                            IsHardToDo = false,
                            TasteRate = 8
                        }

                    }
                },
                new FoodDto()
                {
                    Id = 2,
                    Name = "Chilli con-carne",
                    Description = "Meksykańskie danie z fasoli czerwonej, białej, mięsa mielonego oraz kukurydzy",
                    Recipe = new List<RecipeDto>()
                    {
                        new RecipeDto()
                        {
                            Id = 2,
                            PreparationTime = 20,
                            Ingredients =
                            {
                                "Fasola czerwona 2x","Fasola biała 2x","Kukurydza 1x","Przecier pomidorowy"
                            },
                            IsHardToDo = false,
                            TasteRate = 8.5
                        }
                    }
                },
                new FoodDto()
                {
                    Id = 3,
                    Name = "Mix warzywny",
                    Description = "Danie składające się z losowych warzyw, ryżu, opcjonalnie jakieś mięso",
                    Recipe = new List<RecipeDto>()
                    {
                        new RecipeDto(){
                            Id = 3,
                            PreparationTime = 20,
                            Ingredients =
                            {
                                "Warzywa na patelnie","Ryż","Sól","Pieprz","[Opcjonalnie] dowolne mięso"
                            },
                            IsHardToDo = false,
                            TasteRate = 7,
                        }
                    }
                }
            };
        }
    }
}
