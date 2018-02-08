using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Domain.Concrete
{
    public class EFRecipeIngredientRepository : IRecipeIngredientRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<RecipeIngredient> recipeIngredients
        {
            get
            {
                return context.RecipeIngredients;
            }
        }

        public void SaveRecipeIngredients(RecipeIngredient recipeIngredient)
        {
            if (recipeIngredient.RecipeIngredientID == 0)
            {
                context.RecipeIngredients.Add(recipeIngredient);
            }
            else
            {
                RecipeIngredient dbEntry = context.RecipeIngredients.Find(recipeIngredient.RecipeIngredientID);
                if(dbEntry != null)
                {
                    dbEntry.RecipeID = recipeIngredient.RecipeID;
                    dbEntry.IngredientID = recipeIngredient.IngredientID;
                    dbEntry.UnitOfMeasurementID = recipeIngredient.UnitOfMeasurementID;
                }
            }
            context.SaveChanges();
        }
    }
}
