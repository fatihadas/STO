using STO.Business.Repositories;
using STO.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STO.Business.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly SiparisContext _dbContext;

        public EFUnitOfWork()
        {
            Database.SetInitializer<SiparisContext>(null);
            _dbContext = new SiparisContext();
        }

        public EFUnitOfWork(SiparisContext dbContext)
        {//
            Database.SetInitializer<SiparisContext>(null);

            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext can not be null.");
            }

            _dbContext = dbContext;

            //_dbContext.Configuration.LazyLoadingEnabled = false;
            //_dbContext.Configuration.ValidateOnSaveEnabled = false;
            //_dbContext.Configuration.ProxyCreationEnabled = false;
        }

        #region IUnitOfWork Members
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new EFRepository<T>(_dbContext);
        }

        public int SaveChanges()
        {
            /*try
            {
                BeginTransaction();
                var result = _dbContext.SaveChanges();
                Commit();
                return result;
            }
            catch
            {
                // Burada DbEntityValidationException hatalarını handle edebiliriz.
                Rollback();
                throw;
            }*/
            //return _dbContext.SaveChanges();
            //new usage
            //_dbContext.Database.Connection.Open();
            
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var result = _dbContext.SaveChanges();
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }


        }
        #endregion

        #region IDisposable Members

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /*
        public IRepository<Kullanici> kullaniciRepository;
        public IRepository<Musteri> musteriRepository;

        public IRepository<Kullanici> KullaniciRepository
        {
            get
            {
                return this.kullaniciRepository ?? (this.kullaniciRepository = new EFRepository<Kullanici>(_dbContext));
            }
        }
        public IRepository<Musteri> MusteriRepository
        {
            get
            {
                return this.musteriRepository ?? (this.musteriRepository = new EFRepository<Musteri>(_dbContext));
            }
        }*/

    }
}
