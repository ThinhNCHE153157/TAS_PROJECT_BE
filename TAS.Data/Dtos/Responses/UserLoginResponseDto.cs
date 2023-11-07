using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class UserLoginResponseDto
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        
        public UserLoginResponseDto(int id,string accessToken)
        {
            Id = id;
            AccessToken = accessToken;
        }
    }
}
