using RecipeApp.Domain.Entities;
using RecipeApp.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Domain.Concrete
{
    public class EFIngredientRepository: IIngredientRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Ingredient> Ingredients
        {
            get { return context.Ingredients; }
        }

        public void SaveIngredient(Ingredient ingredient)
        {
            if (ingredient.IngredientID == 0)
            {
                context.Ingredients.Add(ingredient);
            }
            else if(context.Ingredients.Where(i => i.Name == ingredient.Name).FirstOrDefault() != null)
            {
                Ingredient dbEntry = context.Ingredients.Find(ingredient.IngredientID);
                if (dbEntry != null)
                {
                    dbEntry.Name = ingredient.Name;
                }
            }
            context.SaveChanges();
        }
    }
}
