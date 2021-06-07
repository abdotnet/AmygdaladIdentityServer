using Amygdalab.Core.Identity;
using Amygdalab.Data;
using Amygdalab.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Repository
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {

        public UserRoleRepository(DataContext context)
           : base(context)
        {

        }

    }
}
