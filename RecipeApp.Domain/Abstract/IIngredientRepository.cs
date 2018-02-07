using RecipeApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Domain.Abstract
{
    public interface IIngredientRepository
    {
        IEnumerable<Ingredient> Ingredients { get; }
        void SaveIngredient(Ingredient ingredient);
    }
}
