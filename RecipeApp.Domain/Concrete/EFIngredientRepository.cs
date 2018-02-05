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
    }
}
