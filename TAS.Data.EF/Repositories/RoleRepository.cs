using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(TASContext context) : base(context)
        {
        }

        public Role GetRoleById(int id)
        {
            var role = _context.Roles.FirstOrDefault(x => x.RoleId == id);

            // Nếu vai trò không tồn tại, trả về giá trị null
            if (role == null)
            {
                return null;
            }

            // Trả về vai trò
            return role;
        }
    }
}
