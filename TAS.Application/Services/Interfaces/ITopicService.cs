using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface ITopicService
    {
        public Task<Topic> addTopic(AddTopicRequestDto request);
    }
}
