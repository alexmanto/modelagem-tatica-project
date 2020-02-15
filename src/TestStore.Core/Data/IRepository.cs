using System;
using System.Collections.Generic;
using System.Text;
using TestStore.Core.DomainObjects;

namespace ProjectStore.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}