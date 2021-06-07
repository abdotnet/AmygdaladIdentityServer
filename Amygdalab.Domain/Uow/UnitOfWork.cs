using Amygdalab.Data;
using Amygdalab.Domain.Interfaces;
using Amygdalab.Domain.Interfaces.Repositories;
using Amygdalab.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        public IProductRepository Product { get; private set; }
        public IProductHistoryRepository ProductHistory { get; private set; }
        public IRoleRepository Role { get; private set; }
        public IUserRoleRepository UserRole { get; private set; }
        public IUserClaimRepository UserClaim { get; private set; }
        public IUserRepository User { get; private set; }
        public UnitOfWork(DataContext context)
        {
            _context = context;
            Product = new ProductRepository(_context);
            ProductHistory = new ProductHistoryRepository(_context);
            Role = new RoleRepository(_context);
            UserRole = new UserRoleRepository(_context);
            UserClaim = new UserClaimRepository(_context);
            User = new UserRepository(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
