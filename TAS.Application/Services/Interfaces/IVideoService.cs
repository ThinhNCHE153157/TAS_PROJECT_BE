using TAS.Data.Dtos.Requests;

namespace TAS.Application.Services.Interfaces
{
    public interface IVideoService
    {
        public Task<bool> CreateVideo(AddVideoToTopicRequestDto request);
        public Task<bool> DeleteVideo(string videoName);
        public Task<bool> UpdateVideo(UpdateVideoRequestDto request);
        public Task<bool> GetVideo(string videoName);
        public Task<bool> AddVideoToTopic(AddVideoToTopicRequestDto request);
    }
}
