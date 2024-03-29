﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;
using TAS.Infrastructure.Constants;

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
        public bool AddListTopic(List<Topic> topic)
        {
            if (topic != null)
            {
                _context.Topics.AddRange(topic);
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
            return _context.Set<Topic>().Include(x => x.Tests).ThenInclude(x=>x.Parts).Where(x => x.CourseId == courseId);
        }

        public Topic GetTopicByName(string name)
        {
            var topic = _context.Topics.FirstOrDefault(x => x.TopicName == name);
            return topic;
        }

        public IQueryable<Topic> UpdateTopic(Topic topic)
        {
            throw new NotImplementedException();
        }
    }
}
