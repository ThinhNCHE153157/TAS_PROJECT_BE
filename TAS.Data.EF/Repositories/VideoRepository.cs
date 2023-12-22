using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories
{
    public class VideoRepository : BaseRepository<Video>, IVideoRepository
    {
        public VideoRepository(TASContext context) : base(context)
        {
        }

        public Video GetById(int id)
        {
            try
            {
                return _context.Videos.FirstOrDefault(x => x.VideoId == id)!;
            }
            catch (Exception e)
            {
                return null!;
            }
        }

        public bool updateVideo(Video video)
        {
            try
            {
                _context.Videos.Update(video);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}

