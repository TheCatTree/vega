using System;
using System.Threading;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}