using Amygdalab.Core.Identity;
using Amygdalab.Core.Models;
using Amygdalab.Data;
using Amygdalab.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amygdalab.Domain.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DataContext context)
           : base(context)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<Role> GetRoleByName(string roleName)
        {
            var role = await _dataContext.Roles
                         .FirstOrDefaultAsync(c => c.NormalizedName == roleName 
                            || c.Name == roleName);

            return role;
        }
    }
}
