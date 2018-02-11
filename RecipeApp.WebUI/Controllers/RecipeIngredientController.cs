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
            RecipeIngredient recipeIngredient = recipeIngredientRepository.recipeIngredients.Where(r => r.RecipeIngredientID == id).FirstOrDefault();

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
        public ActionResult Edit(RecipeIngredientViewModel recipeIngredientViewModel)
        {
            //Currently if the ingredient or unit of measure change then we add a new unit of measure or ingredient, maybe it should actually update the current ingredient and unit of measure.
            Ingredient ingredient = ingredientRepository.Ingredients.Where(i => i.IngredientID == recipeIngredientViewModel.IngredientID).FirstOrDefault();
            if (recipeIngredientViewModel.IngredientID == 0 || (ingredient != null && ingredient.Name != recipeIngredientViewModel.IngredientName))
            {
                Ingredient ingredientInDBWithSameName = ingredientRepository.Ingredients.Where(i => i.Name == recipeIngredientViewModel.IngredientName).FirstOrDefault();
                if (ingredientInDBWithSameName == null)
                {
                    ingredient = new Ingredient()
                    {
                        Name = recipeIngredientViewModel.IngredientName,
                    };
                    ingredientRepository.SaveIngredient(ingredient);
                    recipeIngredientViewModel.IngredientID = ingredient.IngredientID;
                }
                else
                {
                    ingredient = ingredientInDBWithSameName;
                }
            }
   
            UnitOfMeasurement unitOfMeasurement = unitOfMeasurementRepository.UnitOfMeasurments.Where(u => u.UnitOfMeasurementID == recipeIngredientViewModel.UnitOfMeasurementID).FirstOrDefault();
            if (recipeIngredientViewModel.UnitOfMeasurementID == 0 || (unitOfMeasurement != null && unitOfMeasurement.Name != recipeIngredientViewModel.UnitOfMeasurement))
            {
                UnitOfMeasurement unitOfMeasurementInDBWithSameName = unitOfMeasurementRepository.UnitOfMeasurments.Where(u => u.Name == recipeIngredientViewModel.UnitOfMeasurement).FirstOrDefault();
                if (unitOfMeasurementInDBWithSameName == null)
                {
                    unitOfMeasurement = new UnitOfMeasurement()
                    {
                        Name = recipeIngredientViewModel.UnitOfMeasurement,
                    };
                    unitOfMeasurementRepository.SaveUnitOfMeasurement(unitOfMeasurement);
                    recipeIngredientViewModel.UnitOfMeasurementID = unitOfMeasurement.UnitOfMeasurementID;
                }
                else
                {
                    unitOfMeasurement = unitOfMeasurementInDBWithSameName;
                }
            }

            RecipeIngredient recipeIngredient;
            if (recipeIngredientViewModel.RecipeIngredientID == 0)
            {
                 recipeIngredient = new RecipeIngredient()
                {
                    IngredientID = ingredient.IngredientID,
                    RecipeID = recipeIngredientViewModel.RecipeID,
                    UnitOfMeasurementID = unitOfMeasurement.UnitOfMeasurementID
                };
            }
            else
            {
                recipeIngredient = recipeIngredientRepository.recipeIngredients.Where(r => r.RecipeIngredientID == recipeIngredientViewModel.RecipeIngredientID).FirstOrDefault();
                recipeIngredient.IngredientID = ingredient.IngredientID;
                recipeIngredient.UnitOfMeasurementID = unitOfMeasurement.UnitOfMeasurementID;
            }
            recipeIngredientRepository.SaveRecipeIngredients(recipeIngredient);
            TempData["message"] = string.Format("Ingredient {0} was saved", recipeIngredientViewModel.IngredientName);

            return RedirectToAction("Edit", "Recipe", new { id = recipeIngredient.RecipeID });
        }

        public ActionResult Delete(int id)
        {
            RecipeIngredient dbRecipeIngredient = recipeIngredientRepository.recipeIngredients.Where(r => r.RecipeIngredientID == id).FirstOrDefault();
            if (dbRecipeIngredient == null)
            {
                return HttpNotFound();
            }
            return View(dbRecipeIngredient);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            RecipeIngredient deletedRecipeIngredient = recipeIngredientRepository.DeleteRecipeIngredient(id);
            if (deletedRecipeIngredient != null)
            {
                TempData["message"] = string.Format("Ingredient deleted");
            }
            return RedirectToAction("Edit","Recipe", new { id = deletedRecipeIngredient.RecipeID });
        }

        public ActionResult Details(int id)
        {
            RecipeIngredient dbRecipeIngredient = recipeIngredientRepository.recipeIngredients.Where(r => r.RecipeIngredientID == id).FirstOrDefault();
            if (dbRecipeIngredient == null)
            {
                return HttpNotFound();
            }
            return View(dbRecipeIngredient);
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