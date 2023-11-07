using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        public Role GetRoleById(int id);
    }
}
