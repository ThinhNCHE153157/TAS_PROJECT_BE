using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(TASContext context) : base(context)
        {
        }

        public bool AddTopic(Topic topic)
        {
            if (topic != null)
            {
                _context.Topics.Add(topic);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Topic> DeleteTopic(int topicId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Topic> GetTopicByCourseId(int courseId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Topic> UpdateTopic(Topic topic)
        {
            throw new NotImplementedException();
        }
    }
}
