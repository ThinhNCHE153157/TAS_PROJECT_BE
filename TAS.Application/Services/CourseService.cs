using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;
using TAS.Data.S3Object;
using TAS.Infrastructure.Constants;
using static TAS.Infrastructure.Enums.SystemEnum;

namespace TAS.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IS3StorageService _s3StorageService;

        public CourseService(IUnitOfWork unitOfWork, IMapper mapper, IS3StorageService s3StorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _s3StorageService = s3StorageService;
        }

        public async Task<bool> AddCourse(AddCourseRequestDto request)
        {
            string image = "";
            try
            {
                if (request.Image != null)
                {
                    S3RequestData s3RequestData = new S3RequestData
                    {
                        BucketName = "tas",
                        InputStream = request.Image.OpenReadStream(),
                        Name = request.Image.FileName,
                    };
                    await _s3StorageService.UploadFileAsync(s3RequestData).ConfigureAwait(false);
                    image = _s3StorageService.GetFileUrl(s3RequestData);
                }
                var course = _mapper.Map<Course>(request);
                course.Status = (int)CourseStatus.Draft;
                course.Image = image;
                await _unitOfWork.CourseRepository.AddAsync(course).ConfigureAwait(false);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CourseDashboardResponseDto>> GetAllCourse()
        {
            try
            {
                var listCourse = await _unitOfWork.CourseRepository.GetAllCourses().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<CourseDashboardResponseDto>>(listCourse);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<int?>> GetCourseByAccountId(int accountId)
        {
            try
            {
                if (accountId != 0)
                {
                    var result = _unitOfWork.CourseRepository.GetCourseIdByAccountId(accountId);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }

        public async Task<List<GetEnterpriseCourseResponseDto>> GetCourseByEnterpriseName(string name)
        {
            try
            {
                var result = _unitOfWork.CourseRepository.GetCourseByEnterpriseName(name);
                var result1 = _mapper.Map<List<GetEnterpriseCourseResponseDto>>(result);
                return result1;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<GetCourseByIdResponseDto> GetCourseById(int id)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseById(id).FirstOrDefaultAsync().ConfigureAwait(false);
                var result = _mapper.Map<GetCourseByIdResponseDto>(course);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }

        public async Task<List<CourseHomepageResponeDto>> getCourseHomepage()
        {
            try
            {
                var listCourse = await _unitOfWork.CourseRepository.GetAllCourses().Where(x => x.Status == (int)CourseStatus.Approved && x.IsDeleted == Common.IsNotDelete).ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<CourseHomepageResponeDto>>(listCourse);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> GetCourseIdByName(string name)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var result = await _unitOfWork.CourseRepository.GetCourseIdByName(name).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (result != null)
                    {
                        return result.CourseId;
                    }
                    return 0;
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetEnterpriseNameByAccountId(int id)
        {
            try
            {
                var result = _unitOfWork.AccountRepository.GetEnterpriseNameById(id);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Course>> GetListCourseByAccountId(int id)
        {
            try
            {
                var result = _unitOfWork.CourseRepository.GetListCourseByAccountId(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Course>> GetListCourseByEnterpriseName(string name)
        {
            var result = _unitOfWork.CourseRepository.GetCourseByEnterpriseName(name);
            return result;
        }

        public async Task<bool> UpdateCost(UpdateCostRequestDto request)
        {
            try
            {
                var course = _unitOfWork.CourseRepository.GetCourseById(request.CourseId).FirstOrDefault();
                if (course != null)
                {
                    course.CourseCost = request.Price;
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateStatus(int courseId, int status)
        {
            try
            {
                var course = _unitOfWork.CourseRepository.GetCourseById(courseId).FirstOrDefault();
                if (course != null)
                {
                    course.Status = status;
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
