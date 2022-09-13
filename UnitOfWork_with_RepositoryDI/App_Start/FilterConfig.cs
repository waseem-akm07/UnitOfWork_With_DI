using System.Web;
using System.Web.Mvc;

namespace UnitOfWork_with_RepositoryDI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
