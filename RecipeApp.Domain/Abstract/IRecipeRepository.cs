using RecipeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Domain.Abstract
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> Recipes { get;  }
        void SaveRecipe(Recipe recipe);
    }
}
