using TAS.Data.Dtos.Requests;

namespace TAS.Application.Services.Interfaces
{
    public interface IVideoService
    {
        public Task<bool> CreateVideo(AddVideoToTopicRequestDto request);
        public Task<bool> DeleteVideo(string videoName);
        public Task<bool> UpdateVideo(string videoName, string videoPath);
        public Task<bool> GetVideo(string videoName);
    }
}
