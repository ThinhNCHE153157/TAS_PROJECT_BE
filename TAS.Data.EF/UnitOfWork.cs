using TAS.Data.EF.Repositories;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool disposed = false;

        private readonly TASContext _context;

        private IAccountRepository _accountRepository;
        private ICourseRepository _courseRepository;
        private IQuestionRepository _questionRepository;
        private IRoleRepository _roleRepositery;
        private ITestRepository _testRepository;
        private ITopicRepository _topicRepository;
        private IVideoRepository _videoRepository;
        private IOrderRepository _orderRepository;

        public UnitOfWork(TASContext context)
        {
            _context = context;
        }


        public IAccountRepository AccountRepository
        {
            get
            {
                if (this._accountRepository is null)
                {
                    this._accountRepository = new AccountRepository(_context);
                }
                return _accountRepository;
            }
        }

        public ICourseRepository CourseRepository
        {
            get
            {
                if (this._courseRepository is null)
                {
                    this._courseRepository = new CourseRepository(_context);
                }
                return _courseRepository;
            }
        }
        public IQuestionRepository QuestionRepository
        {
            get
            {
                if (this._questionRepository is null)
                {
                    this._questionRepository = new QuestionRepository(_context);
                }
                return _questionRepository;
            }
        }


        public IRoleRepository RoleRepositery
        {
            get
            {
                if (this._roleRepositery is null)
                {
                    this._roleRepositery = new RoleRepository(_context);
                }
                return _roleRepositery;
            }
        }

        public ITestRepository TestRepository
        {
            get
            {
                if (this._testRepository is null)
                {
                    this._testRepository = new TestRepository(_context);
                }
                return _testRepository;
            }
        }

        public ITopicRepository TopicRepository
        {
            get
            {
                if (this._topicRepository is null)
                {
                    this._topicRepository = new TopicRepository(_context);
                }
                return _topicRepository;
            }
        }

        public IVideoRepository VideoRepository
        {
            get
            {
                if (this._videoRepository is null)
                {
                    this._videoRepository = new VideoRepository(_context);
                }
                return _videoRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (this._orderRepository is null)
                {
                    this._orderRepository = new OrderRepository(_context);
                }
                return _orderRepository;
            }
        }

        

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}