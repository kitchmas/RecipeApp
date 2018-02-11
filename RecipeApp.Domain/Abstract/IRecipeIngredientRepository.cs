using RecipeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Domain.Abstract
{
    public interface IRecipeIngredientRepository
    {
        IEnumerable<RecipeIngredient> recipeIngredients { get; }
        void SaveRecipeIngredients(RecipeIngredient recipeIngredient);
        RecipeIngredient DeleteRecipeIngredient(int id);
    }
}
