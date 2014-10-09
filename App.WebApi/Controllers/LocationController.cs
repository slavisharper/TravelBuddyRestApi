namespace App.WebApi.Controllers
{
    using App.Data;
    using App.WebApi.Models;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class LocationController : BaseController
    {
        public LocationController(ITravelBuddyData data)
            :base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            string userId = this.User.Identity.GetUserId();
            var user = this.data.Users.All().FirstOrDefault(u => u.Id == userId);
            var location = new LocationModel
            {
                Latitude = user.Longtitude,
                Longtitude = user.Latitude
            };

            return Ok(location);
        }

        [HttpPut]
        public IHttpActionResult Update(LocationModel newLocation)
        {
            string userId = this.User.Identity.GetUserId();
            var user = this.data.Users.All().FirstOrDefault(u => u.Id == userId);
            if (newLocation.Longtitude == null || newLocation.Latitude == null)
            {
                return BadRequest("Invalid location!");
            }

            user.Latitude = newLocation.Latitude;
            user.Longtitude = newLocation.Longtitude;

            return Ok();
        }
    }
}
