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
        private IRecipeRepository repository;

        public RecipeController(IRecipeRepository recipeRepository)
        {
            this.repository = recipeRepository;
        }

        // GET: Recipe
        public ActionResult Index(int? id)
        {
            var viewModel = new RecipesViewModel()
            {
                Recipes = repository.Recipes.OrderBy(r => r.Name),
            };

            if (id != null)
            {
                viewModel.RecipeIngredients = viewModel.Recipes.Where(r => r.RecipeID == id).Single().RecipeIngredients;
            }

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View("Edit",new Recipe());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Description, Method")]Recipe recipe)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.SaveRecipe(recipe);
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(recipe);
        }

        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Recipe recipe = repository.Recipes.Where(r => r.RecipeID == id).FirstOrDefault();
            if(recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }
    }
}