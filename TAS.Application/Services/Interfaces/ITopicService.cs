﻿using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface ITopicService
    {
        public Task<Topic> addTopic(AddTopicRequestDto request);
        public Task<List<Topic>> addListTopic(List<AddTopicRequestDto> request);
        public Task<List<GetTopicByCourseIdResponseDto>> getListTopicByCourseId(int courseId);
        public Task<Topic> GetTopicByName(string name);
    }
}
