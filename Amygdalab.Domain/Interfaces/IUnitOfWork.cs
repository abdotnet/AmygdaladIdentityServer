using Amygdalab.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        IProductHistoryRepository ProductHistory { get; }
        IRoleRepository Role { get; }
        IUserRoleRepository UserRole { get; }
        IUserClaimRepository UserClaim { get; }
        IUserRepository User { get; }

        Task<int> CompleteAsync();
    }
}
