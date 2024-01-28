using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface ITopicRepository : IBaseRepository<Topic>
    {
        public IQueryable<Topic> GetTopicByCourseId(int courseId);
        public bool AddTopic(Topic topic);
        public bool AddListTopic(List<Topic> topic);
        public IQueryable<Topic> UpdateTopic(Topic topic);
        public IQueryable<Topic> DeleteTopic(int topicId);
        public Topic GetTopicByName(string name);

    }
}
