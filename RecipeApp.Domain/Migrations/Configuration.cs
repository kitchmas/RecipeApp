namespace RecipeApp.Domain.Migrations
{
    using Domain.Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RecipeApp.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RecipeApp.Domain.Concrete.EFDbContext context)
        {
            var recipes = new List<Recipe>
            {
                new Recipe { Name = "Spaghetti Bolognese", Description = "A Tasty Pasta Dish", Method = "1. Make Spaghetti Bolognese. 2. Done."},
                new Recipe { Name = "Cheese Pizza", Description = "A Tasty Pizza Dish", Method = "1. Make Cheese Pizza. 2. Done."},
                new Recipe { Name = "Chicken Risotto", Description = "A Tasty Rice Dish", Method = "1. Make Chicken Risotto. 2. Done."}
            };

            recipes.ForEach(r => context.Recipes.AddOrUpdate(p => p.Name, r));
            context.SaveChanges();

            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Pasta" },
                new Ingredient { Name = "Tomatoes" },
                new Ingredient { Name = "Beef Mince Meat" },
                new Ingredient { Name = "Cheese" },
                new Ingredient { Name = "Flour" },
                new Ingredient { Name = "Water" },
                new Ingredient { Name = "Chicken" },
                new Ingredient { Name = "White Rice" },
                new Ingredient { Name = "Chicken Stock" }
            };
            ingredients.ForEach(i => context.Ingredients.AddOrUpdate(p => p.Name, i));
            context.SaveChanges();

            var unitOfMeasurements = new List<UnitOfMeasurement>
            {
                new UnitOfMeasurement { Name = "teaspoon" },
                new UnitOfMeasurement { Name = "tablespoon" },
                new UnitOfMeasurement { Name = "fluid ounce" },
                new UnitOfMeasurement { Name = "gill" },
                new UnitOfMeasurement { Name = "cup" },
                new UnitOfMeasurement { Name = "pint" },
                new UnitOfMeasurement { Name = "quart" },
                new UnitOfMeasurement { Name = "gallon" },
                new UnitOfMeasurement { Name = "dl" },
                new UnitOfMeasurement { Name = "pound" },
                new UnitOfMeasurement { Name = "ounce" },
                new UnitOfMeasurement { Name = "mg" },
                new UnitOfMeasurement { Name = "g" },
                new UnitOfMeasurement { Name = "kg" }
            };
            unitOfMeasurements.ForEach(u => context.UnitsOfMeasurements.AddOrUpdate(p => p.Name, u));
            context.SaveChanges();

            var recipeIngredients = new List<RecipeIngredient>
            {
                //new RecipeIngredient {
                //    RecipeID = recipes.Single(r => r.Name == "Spaghetti Bolognese").RecipeID,
                //    IngredientID = ingredients.Single(i => i.Name =="Pasta").IngredientID,
                //    UnitOfMeasurementID = unitOfMeasurements.Single(u => u.Name == "g").UnitOfMeasurementID
                //},
                //new RecipeIngredient {
                //    RecipeID = recipes.Single(r => r.Name == "Spaghetti Bolognese").RecipeID,
                //    IngredientID = ingredients.Single(i => i.Name =="Tomatoes").IngredientID,
                //    UnitOfMeasurementID = unitOfMeasurements.Single(u => u.Name == "g").UnitOfMeasurementID
                //},
                //new RecipeIngredient
                //{
                //    RecipeID = recipes.Single(r => r.Name == "Spaghetti Bolognese").RecipeID,
                //    IngredientID = ingredients.Single(i => i.Name == "Beef Mince Meat").IngredientID,
                //    UnitOfMeasurementID = unitOfMeasurements.Single(u => u.Name == "kg").UnitOfMeasurementID
                //},
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Cheese Pizza").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Cheese").IngredientID,
                    UnitOfMeasurementID = unitOfMeasurements.Single(u => u.Name == "cup").UnitOfMeasurementID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Cheese Pizza").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Flour").IngredientID,
                    UnitOfMeasurementID = unitOfMeasurements.Single(u => u.Name == "g").UnitOfMeasurementID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Cheese Pizza").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Water").IngredientID,
                    UnitOfMeasurementID = unitOfMeasurements.Single(u => u.Name == "cup").UnitOfMeasurementID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Chicken Risotto").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Chicken").IngredientID,
                    UnitOfMeasurementID = unitOfMeasurements.Single(u => u.Name == "pound").UnitOfMeasurementID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Chicken Risotto").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "White Rice").IngredientID,
                    UnitOfMeasurementID = unitOfMeasurements.Single(u => u.Name == "cup").UnitOfMeasurementID
                },
                new RecipeIngredient
                {
                    RecipeID = recipes.Single(r => r.Name == "Chicken Risotto").RecipeID,
                    IngredientID = ingredients.Single(i => i.Name == "Chicken Stock").IngredientID,
                    UnitOfMeasurementID = unitOfMeasurements.Single(u => u.Name == "cup").UnitOfMeasurementID
                }
            };

            //foreach (RecipeIngredient e in recipeIngredients)
            //{
            //    var recipeIngredientDataBase = context.RecipeIngredients.Where(
            //        r => r.Recipe.RecipeID == e.Recipe.RecipeID &&
            //             r.Ingredient.IngredientID == e.Ingredient.IngredientID
            //             && r.UnitOfMeasurement.UnitOfMeasurementID == e.UnitOfMeasurement.UnitOfMeasurementID
            //             ).SingleOrDefault();
            //    if (recipeIngredientDataBase == null)
            //    {
            //        context.RecipeIngredients.Add(e);
            //    }
            //}

            foreach (RecipeIngredient e in recipeIngredients)
            {
                var recipeIngredientDataBase = context.RecipeIngredients.Where(
                    s => s.RecipeID == e.RecipeID &&
                         s.IngredientID == e.IngredientID &&
                         s.UnitOfMeasurementID == e.UnitOfMeasurementID).SingleOrDefault();
                if (recipeIngredientDataBase == null)
                {
                    context.RecipeIngredients.Add(e);
                }
            }

            context.SaveChanges();
        }
    }
}
