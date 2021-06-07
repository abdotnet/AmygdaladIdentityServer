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
    public class UserClaimRepository : BaseRepository<UserClaim>, IUserClaimRepository
    {
        public UserClaimRepository(DataContext context)
          : base(context)
        {

        }
    }
}
