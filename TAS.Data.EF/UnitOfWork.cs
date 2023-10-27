﻿using TAS.Data.EF.Repositories;
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