using System.Web;
using System.Web.Mvc;

namespace Mooshak2_Hopur5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
