using System;
using RecipeApp.Domain.Abstract;
using RecipeApp.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Domain.Concrete
{
    public class EFUnitOfMeasurmentRepository : IUnitOfMeasurementRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<UnitOfMeasurement> UnitOfMeasurments
        {
            get { return context.UnitsOfMeasurements; }
        }
        public void SaveUnitOfMeasurement(UnitOfMeasurement unitOfMeasurement)
        {
            if (unitOfMeasurement.UnitOfMeasurementID == 0)
            {
                context.UnitsOfMeasurements.Add(unitOfMeasurement);
            }
            else
            {
                UnitOfMeasurement dbEntry = context.UnitsOfMeasurements.Find(unitOfMeasurement.UnitOfMeasurementID);
                if (dbEntry != null)
                {
                    dbEntry.Name = unitOfMeasurement.Name;
                }
            }
            context.SaveChanges();
        }
    }
}
