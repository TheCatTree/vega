using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleAsync(int id, bool includeRelated = true);

        Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery query);

        void Add(Vehicle vehicle);

        void Remove(Vehicle vehicle);
    }
}