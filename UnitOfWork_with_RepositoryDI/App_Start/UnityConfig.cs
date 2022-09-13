using System.Web.Mvc;
using UnitOfWork;
using Unity;
using Unity.Mvc5;

namespace UnitOfWork_with_RepositoryDI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // applying dependency injuction 
             container.RegisterType<IUnitOfWorkRepo, UnitOfWorkRepo>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}