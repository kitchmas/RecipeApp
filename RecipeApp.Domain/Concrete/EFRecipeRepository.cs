using RecipeApp.Domain.Abstract;
using RecipeApp.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace RecipeApp.Domain.Concrete
{
    public class EFRecipeRepository : IRecipeRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Recipe> Recipes
        {
            get { return context.Recipes.Include(r => r.RecipeIngredients); }
        }

        public void SaveRecipe(Recipe recipe)
        {
            if (recipe.RecipeID == 0)
            {
                context.Recipes.Add(recipe);
            }
            else
            {
                Recipe dbEntry = context.Recipes.Find(recipe.RecipeID);
                if(dbEntry != null)
                {
                    dbEntry.Name = recipe.Name;
                    dbEntry.Description = recipe.Description;
                    dbEntry.Method = recipe.Method;
                }
            }
            context.SaveChanges();
           
        }
    }
}
