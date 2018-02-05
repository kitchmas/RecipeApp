using RecipeApp.Domain.Entities;
using System.Data.Entity;

namespace RecipeApp.Domain.Concrete
{
    public class EFDbContext: DbContext
    {
        public EFDbContext() : base("name=EFDbContext") { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<UnitOfMeasurement> UnitsOfMeasurements { get; set; }
    }
}
