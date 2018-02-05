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

        public RecipeIngredientController(IIngredientRepository IngredientRepository, IUnitOfMeasurementRepository UnitOfMeasurementRepository)
        {
            this.ingredientRepository = IngredientRepository;
            this.unitOfMeasurementRepository = UnitOfMeasurementRepository;
        }

        // GET: RecipeIngredient
        public ActionResult Create()
        {
            PopulateIngredientsDropDown();
            PopulateUnitOfMeasurmentDropDown();
            return View("Edit", new RecipeIngredient());
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