using vega.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Persistence
{
     public class UnitOfWork : IUnitOfWork 
    {
        private readonly VegaDbContext context;

        public UnitOfWork(VegaDbContext context){
            this.context = context;
            
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}