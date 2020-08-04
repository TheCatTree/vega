using System;
using System.Threading;
using System.Threading.Tasks;
using vega.Models;

namespace vega.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}