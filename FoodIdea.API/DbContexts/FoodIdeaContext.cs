using FoodIdea.API.Entities;
using Microsoft.EntityFrameworkCore;


namespace FoodIdea.API.DbContexts
{
    public class FoodIdeaContext : DbContext
    {
        public DbSet<Food> Foods { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;

        public FoodIdeaContext(DbContextOptions<FoodIdeaContext> options)
    : base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySql(ServerVersion.AutoDetect("server = localhost; userid = root;" +
        //        " pwd = UxEEPdQA9lQB2aNnLgUa; port = 3306; database = foodIdea_db"));
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Food>()
                .HasData(
                new Food("Spaghetti")
                {
                    Id = 1,
                    Description = "Włoskie danie z makaronem i mięsem mielonym"
                },
                new Food("Chilli con-carne")
                {
                    Id = 2,
                    Description = "Meksykańskie danie z fasoli czerwonej, białej, mięsa mielonego oraz kukurydzy"
                },
                new Food("Mix warzywny")
                {
                    Id = 3,
                    Description = "Danie składające się z losowych warzyw, ryżu, opcjonalnie jakieś mięso"
                });           
            modelBuilder.Entity<Recipe>()
                .HasData(
                new Recipe()
                {
                    Id = 1,
                    FoodId = 1,
                    PreparationTime = 30,
                    IngredientsList = { "tomato" },
                    IsHardToDo = false,
                    TasteRate = 8,
                    HowToMake = "Ugotuj makaron, usmaz mieso, dodaj sos"
                },
                new Recipe()
                {
                    Id = 2,
                    FoodId = 2,
                    PreparationTime = 20,
                    IngredientsList =
                            {
                                "Fasola czerwona 2x","Fasola biała 2x","Kukurydza 1x","Przecier pomidorowy"
                            },
                    IsHardToDo = false,
                    TasteRate = 8.5,
                    HowToMake = "Usmaz mieso, dodaj fasole, zrob sos"
                },
                new Recipe()
                {
                    Id = 3,
                    FoodId = 3,
                    PreparationTime = 20,
                    IngredientsList =
                            {
                                "Warzywa na patelnie","Ryż","Sól","Pieprz","[Opcjonalnie] dowolne mięso"
                            },
                    IsHardToDo = false,
                    TasteRate = 7,
                    HowToMake = "Usmaz mieso, dodaj warzywa, dodaj odpowiednie przyprawy"
                });
            base.OnModelCreating(modelBuilder);
        }

    }
}
