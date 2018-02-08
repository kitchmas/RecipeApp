using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecipeApp.Domain.Abstract;
using RecipeApp.WebUI.Models;
using RecipeApp.Domain.Entities;
using System.Data.Entity.Infrastructure;


namespace RecipeApp.WebUI.Controllers
{
    public class RecipeIngredientController : Controller
    {
        private IIngredientRepository ingredientRepository;
        private IUnitOfMeasurementRepository unitOfMeasurementRepository;
        private IRecipeIngredientRepository recipeIngredientRepository;

        public RecipeIngredientController(IIngredientRepository ingredientRepository,
            IUnitOfMeasurementRepository unitOfMeasurementRepository,
            IRecipeIngredientRepository recipeIngredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
            this.unitOfMeasurementRepository = unitOfMeasurementRepository;
            this.recipeIngredientRepository = recipeIngredientRepository;
        }

        // GET: RecipeIngredient
        public ActionResult Create(int id)
        {
            RecipeIngredientViewModel recipeIngredient = new RecipeIngredientViewModel();
            recipeIngredient.RecipeID = id;

            return View("Edit", recipeIngredient);
        }

        public ActionResult Edit(int id)
        {
            RecipeIngredient ingredient = recipeIngredientRepository.recipeIngredients.Where(r => r.RecipeIngredientID == id).FirstOrDefault();

            RecipeIngredientViewModel recipeIngredientViewModel = new RecipeIngredientViewModel()
            {
                RecipeIngredientID = recipeIngredient.RecipeIngredientID,
                RecipeID = recipeIngredient.RecipeID,
                IngredientID = recipeIngredient.IngredientID,
                UnitOfMeasurementID = recipeIngredient.UnitOfMeasurementID,
                IngredientName = recipeIngredient.Ingredient.Name,
                UnitOfMeasurement = recipeIngredient.UnitOfMeasurement.Name
            };

            return View("Edit", recipeIngredientViewModel);
        }

        [HttpPost]
        public ActionResult EditPost(RecipeIngredientViewModel recipeIngredientViewModel)
        {
            //Check if there is an ingredient and Unit of measurment are already created and if not make a new one.
            Ingredient ingredient;
            UnitOfMeasurement unitOfMeasurement;

            if (recipeIngredientViewModel.IngredientID == 0)
            {
                ingredient = new Ingredient()
                {
                    Name = recipeIngredientViewModel.IngredientName
                };
            }
            else
            {
                ingredient = ingredientRepository.Ingredients.Where(i => i.IngredientID == recipeIngredientViewModel.IngredientID).FirstOrDefault();
            }
            ingredientRepository.SaveIngredient(ingredient);
            if (recipeIngredientViewModel.UnitOfMeasurementID == 0)
            {
                unitOfMeasurement = new UnitOfMeasurement()
                {
                    Name = recipeIngredientViewModel.UnitOfMeasurement
                };
            }
            else
            {
                unitOfMeasurement = unitOfMeasurementRepository.UnitOfMeasurments.Where(u => u.UnitOfMeasurementID == recipeIngredientViewModel.UnitOfMeasurementID).FirstOrDefault();
            }
            unitOfMeasurementRepository.SaveUnitOfMeasurement(unitOfMeasurement);

            RecipeIngredient recipeIngredient = new RecipeIngredient()
            {
                IngredientID = ingredient.IngredientID,
                RecipeID = recipeIngredientViewModel.RecipeID,
                UnitOfMeasurementID = unitOfMeasurement.UnitOfMeasurementID
            };
            recipeIngredientRepository.SaveRecipeIngredients(recipeIngredient);

            TempData["message"] = string.Format("Ingredient {0} was saved", recipeIngredient.Ingredient.Name);

            return RedirectToAction("Index");
        }

        private void PopulateIngredientsDropDown(object selectedIngredient = null)
        {
            var ingredientQuery = from i in ingredientRepository.Ingredients
                                  orderby i.Name
                                  select i;
            ViewBag.IngredientID = new SelectList(ingredientQuery, "IngredientID", "Name", selectedIngredient);
        }

        private void PopulateUnitOfMeasurmentDropDown(object selectedunitOfMeasurement = null)
        {
            var unitOfMeasurementQuery = from i in unitOfMeasurementRepository.UnitOfMeasurments
                                         orderby i.Name
                                         select i;
            ViewBag.UnitOfMeasurementID = new SelectList(unitOfMeasurementQuery, "UnitOfMeasurementID", "Name", selectedunitOfMeasurement);
        }
    }
}