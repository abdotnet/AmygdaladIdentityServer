using Amygdalab.Core.Identity;
using Amygdalab.Data;
using Amygdalab.Domain.Interfaces.Repositories;
using Amygdalab.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Repository
{
   public class UserRepository :  BaseRepository<User>, IUserRepository
    {

        public UserRepository(DataContext context)
           : base(context)
        {

        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await GetAll();
            return users.OrderBy(u => u.Email).ToList();
        }
        public async Task<List<User>> FindUserByUserName(string username)
        {
            var users = await _dataContext.Users.Where(c => c.UserName == username).ToListAsync();
            return users;
        }
    }
}
