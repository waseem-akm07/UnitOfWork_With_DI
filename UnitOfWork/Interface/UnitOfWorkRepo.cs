using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessAccessLayer;
using BusinessAccessLayer.Repository;
using DataAccessLayer.Model;

namespace UnitOfWork
{
    public class UnitOfWorkRepo : IUnitOfWorkRepo, IDisposable
    {
        private BusinessLayer _BusinessLayer;
        private MvcdbEntities _entities = new MvcdbEntities();

        //private MvcdbEntities _entities;

        /// <summary>
        /// Constructore to initialize the DbContext
        /// </summary>
        /// <param name="entities"> DbContext</param>
        //public UnitOfWorkRepo(MvcdbEntities entities)
        //{
        //    _entities = entities;
        //    BusinessLayer = new BusinessLayer(_entities);
        //}

        /// <summary>
        /// To Access BusinessAccessLayer Interface 
        /// </summary>
        //public IBusinessLayer BusinessLayer { get; set; }
        public IBusinessLayer BusinessLayer
        {
            get
            {
                if (_BusinessLayer == null)
                {
                    _BusinessLayer = new BusinessLayer(_entities);
                }
                return _BusinessLayer;
            }
        }

        //  private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed)
        //    {
        //        if (disposing)
        //        {
        //            _entities.Dispose();
        //        }
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        /// <summary>
        /// To save data in database
        /// </summary>
        public void Commit()
        {
            _entities.SaveChanges();
        }

        /// <summary>
        /// Return exception if transection is failed
        /// </summary>
        /// <returns> Error Exception </returns>
        public string Rollback()
        {
            return "Transection Failed";
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
