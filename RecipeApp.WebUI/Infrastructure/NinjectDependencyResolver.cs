using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using RecipeApp.Domain.Abstract;
using RecipeApp.Domain.Concrete;
using RecipeApp.Domain.Entities;

namespace RecipeApp.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IRecipeRepository>().To<EFRecipeRepository>();
            kernel.Bind<IIngredientRepository>().To<EFIngredientRepository>();
            kernel.Bind<IUnitOfMeasurementRepository>().To<EFUnitOfMeasurmentRepository>();
            kernel.Bind<IRecipeIngredientRepository>().To<EFRecipeIngredientRepository>();
        }
    }
}