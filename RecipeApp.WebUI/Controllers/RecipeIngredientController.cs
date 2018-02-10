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
            

            Ingredient ingredient = ingredientRepository.Ingredients.Where(i => i.IngredientID == recipeIngredientViewModel.IngredientID).FirstOrDefault();
            Ingredient ingredientInDBWithSameName = ingredientRepository.Ingredients.Where(i => i.Name == recipeIngredientViewModel.IngredientName).FirstOrDefault();
            //Checks if the ingredient has new Id
            //if (recipeIngredientViewModel.IngredientID == 0 || (ingredientRepository.Ingredients.Where(i => i.IngredientID == recipeIngredientViewModel.IngredientID).FirstOrDefault().Name != recipeIngredientViewModel.IngredientName))
            if (recipeIngredientViewModel.IngredientID == 0 || (ingredient.Name != recipeIngredientViewModel.IngredientName))
            {
                //check if ingreident with the same name already exist in the db
                if (ingredientInDBWithSameName == null)
                {
                    ingredient = new Ingredient()
                    {
                        Name = recipeIngredientViewModel.IngredientName,
                    };
                    ingredientRepository.SaveIngredient(ingredient);
                    recipeIngredientViewModel.IngredientID = ingredient.IngredientID;
                }
                //if it exists set the ingredient to the db ingredient
                else
                {
                    ingredient = ingredientInDBWithSameName;
                }
            }
            ////if the id is not 0 check if the name of the ingredient has changed.
            //else if (ingredientRepository.Ingredients.Where(i => i.IngredientID == recipeIngredientViewModel.IngredientID).FirstOrDefault().Name != recipeIngredientViewModel.IngredientName)
            //{
            //    //If the name has changed check it exists in the db
            //    if (IngredientInDBWithSameName == null)
            //    {
            //        ingredient = new Ingredient()
            //        {
            //            Name = recipeIngredientViewModel.IngredientName,
            //        };
            //        ingredientRepository.SaveIngredient(ingredient);
            //        recipeIngredientViewModel.IngredientID = ingredient.IngredientID;
            //    }
            //    //if it exists set the ingredient to the db ingredient
            //    else
            //    {
            //        ingredient = IngredientInDBWithSameName;
            //    }
            //}
            //Checks if unit of measure has new Id
            UnitOfMeasurement unitOfMeasurement = unitOfMeasurementRepository.UnitOfMeasurments.Where(u => u.UnitOfMeasurementID == recipeIngredientViewModel.UnitOfMeasurementID).FirstOrDefault();
            UnitOfMeasurement unitOfMeasurementInDBWithSameName = unitOfMeasurementRepository.UnitOfMeasurments.Where(u => u.Name == recipeIngredientViewModel.UnitOfMeasurement).FirstOrDefault();

            if (recipeIngredientViewModel.UnitOfMeasurementID == 0 || (unitOfMeasurement.Name != recipeIngredientViewModel.UnitOfMeasurement))
            {
                //check if ingreident with the same name already exist in the db
                if (unitOfMeasurementInDBWithSameName == null)
                {
                    unitOfMeasurement = new UnitOfMeasurement()
                    {
                        Name = recipeIngredientViewModel.UnitOfMeasurement,
                    };
                    unitOfMeasurementRepository.SaveUnitOfMeasurement(unitOfMeasurement);
                    recipeIngredientViewModel.UnitOfMeasurementID = unitOfMeasurement.UnitOfMeasurementID;
                }
                //if it exists set the ingredient to the db ingredient
                else
                {
                    unitOfMeasurement = unitOfMeasurementInDBWithSameName;
                }
            }

            ////Copy of orginal
            //if (recipeIngredientViewModel.UnitOfMeasurementID == 0)
            //{
            //    //if it is a new id check that the unit of measure isn't already in the db
            //    if (unitOfMeasurementRepository.UnitOfMeasurments.Where(i => i.Name == recipeIngredientViewModel.UnitOfMeasurement).FirstOrDefault() == null)
            //    {
            //        unitOfMeasurement = new UnitOfMeasurement()
            //        {
            //            Name = recipeIngredientViewModel.UnitOfMeasurement,
            //        };
            //        unitOfMeasurementRepository.SaveUnitOfMeasurement(unitOfMeasurement);
            //    }
            //    // if it is in the database update the unit of measure to the db unit of measure
            //    else
            //    {
            //        unitOfMeasurement = unitOfMeasurementRepository.UnitOfMeasurments.Where(i => i.Name == recipeIngredientViewModel.UnitOfMeasurement).FirstOrDefault();
            //    }
            //}
            ////if the id is not 0 check if the name of the unit of measure has changed.
            //else if (unitOfMeasurementRepository.UnitOfMeasurments.Where(u => u.UnitOfMeasurementID == recipeIngredientViewModel.UnitOfMeasurementID).FirstOrDefault().Name != recipeIngredientViewModel.UnitOfMeasurement)
            //{
            //    //if has changed check that if it already exists in the db
            //    if (unitOfMeasurementRepository.UnitOfMeasurments.Where(i => i.Name == recipeIngredientViewModel.UnitOfMeasurement).FirstOrDefault() == null)
            //    {
            //        unitOfMeasurement = new UnitOfMeasurement()
            //        {
            //            Name = recipeIngredientViewModel.UnitOfMeasurement,
            //        };
            //        unitOfMeasurementRepository.SaveUnitOfMeasurement(unitOfMeasurement);
            //    }
            //    //if dose already exists set the unit of measure to the unit of measure in the database.
            //    else
            //    {
            //        unitOfMeasurement = unitOfMeasurementRepository.UnitOfMeasurments.Where(i => i.Name == recipeIngredientViewModel.UnitOfMeasurement).FirstOrDefault();
            //    }
            //}
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