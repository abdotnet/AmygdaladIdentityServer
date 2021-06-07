using Amygdalab.Core.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Interfaces.Repositories
{
    public interface IUserRepository :IRepository<User>
    {
        Task<List<User>> FindUserByUserName(string username);
    }
}
