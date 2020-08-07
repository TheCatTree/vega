using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using vega.Core;
using vega.Models;
using System.Linq;
using System.Linq.Expressions;
using vega.Extensions;
using vega.Core.Models;

namespace vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;

        }

        public async Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true){
            
            if(includeRelated == false){
                return await context.Vehicles.FindAsync(id);
            }
            return await context.Vehicles
            .Include(v => v.Model)
                .ThenInclude(m => m.Make)
            .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
            .SingleOrDefaultAsync(v => v.Id == id); 
        }

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObject)
        {
            var query = context.Vehicles
            .Include(v => v.Model)
                .ThenInclude(m => m.Make)
            .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
            .AsQueryable(); 

            if (queryObject.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObject.MakeId.Value);

            var columnsMap = new Dictionary<string,Expression<Func<Vehicle, object>>>{
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
            };


            query = query.ApplyOrdering(queryObject,columnsMap);

            query = query.ApplyPaging(queryObject);
            return await query.ToListAsync();
        }



        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}