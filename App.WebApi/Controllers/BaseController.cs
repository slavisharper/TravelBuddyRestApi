namespace App.WebApi.Controllers
{
    using App.Data;
    using System.Web.Http;

    public class BaseController : ApiController
    {
        protected ITravelBuddyData data;

        public BaseController(ITravelBuddyData data)
        {
            this.data = data;
        }
    }
}
