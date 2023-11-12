using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.Services.Interfaces;
using TAS.Data.EF;
using TAS.Data.Entities;

namespace TAS.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Role> GetRoleById(int RoleId)

        {
            try {
                var role = _unitOfWork.RoleRepositery.GetRoleById(RoleId);
                if(role != null) 
                {
                    return role;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
