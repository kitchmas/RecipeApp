using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecipeApp.Domain.Entities;

namespace RecipeApp.WebUI.Models
{
    public class RecipesViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<RecipeIngredient> RecipeIngredients { get; set; }
    }
}