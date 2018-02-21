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
    public class RecipeController : Controller
    {
        private IRecipeRepository recipeRepository;
        private IRecipeIngredientRepository recipeIngredientRepository;

        public RecipeController(IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository)
        {
            this.recipeRepository = recipeRepository;
            this.recipeIngredientRepository = recipeIngredientRepository;
        }

        // GET: Recipe
        public ActionResult Index()
        {
            var viewModel = new RecipesViewModel()
            {
                Recipes = recipeRepository.Recipes.OrderBy(r => r.Name)
            };
            //if (id != null)
            //{
            //    viewModel.RecipeIngredients = viewModel.Recipes.Where(r => r.RecipeID == id).Single().RecipeIngredients;
            //}

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            Recipe recipe = recipeRepository.Recipes.Where(r => r.RecipeID == id).FirstOrDefault();
            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }

        public ActionResult Create()
        {
            return View("Edit",new Recipe());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Recipe recipe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    recipeRepository.SaveRecipe(recipe);
                    return View(recipe);
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(recipe);
        }

        public ActionResult Edit(int id)
        {
            Recipe recipe = recipeRepository.Recipes.Where(r => r.RecipeID == id).FirstOrDefault();
            if(recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Recipe dbRecipe = recipeRepository.Recipes.Where(r => r.RecipeID == id).FirstOrDefault();
            if(dbRecipe == null)
            {
                return HttpNotFound();
            }
            return View(dbRecipe);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            Recipe deletedRecipe = recipeRepository.DeleteRecipe(id);

            if(deletedRecipe != null)
            {
                //Delete the recipies recipe ingredients.
                var recipeIngredients = recipeIngredientRepository.recipeIngredients.Where(r => r.RecipeID == deletedRecipe.RecipeID).ToList();

                foreach(var recipeIngredient in recipeIngredients)
                {
                    recipeIngredientRepository.DeleteRecipeIngredient(recipeIngredient.RecipeIngredientID);
                }

                TempData["message"] = string.Format("{0} was deleted", deletedRecipe.Name);
            }
            return RedirectToAction("Index");
        }
    }
}