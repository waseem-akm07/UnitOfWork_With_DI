using BusinessAccessLayer.Repository;

namespace UnitOfWork
{
    public interface IUnitOfWorkRepo
    {
        // BusinessAccessLayer Interface  
        IBusinessLayer BusinessLayer { get; }
        void Commit();
        string Rollback();      
    }
}
