using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeApp.WebUI.Models
{
    public class RecipeIngredientViewModel
    {
        public int RecipeIngredientID { get; set; }
        public int RecipeID { get; set; }
        public int IngredientID { get; set; }
        public int UnitOfMeasurementID { get; set; }
        public string IngredientName { get; set;}
        public string UnitOfMeasurement { get; set; }
    }
}