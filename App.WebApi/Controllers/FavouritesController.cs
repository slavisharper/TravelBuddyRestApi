namespace App.WebApi.Controllers
{
    using App.Data;
    using App.WebApi.Models;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Web.Http;
    using System.Collections.Generic;
    using App.Models;

    [Authorize]
    public class FavouritesController : BaseController
    {
        public FavouritesController(ITravelBuddyData data)
            :base(data)
        {
        }
        
        [HttpGet]
        public IHttpActionResult GetUserFavourites()
        {
            var userId = this.User.Identity.GetUserId();
            var user = this.data.Users.All().FirstOrDefault(u => u.Id == userId);
            var result = user.Favourites.AsQueryable().Select(PlaceModel.FromPlace);
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult AddFavourite(int placeId)
        {
            var place = this.data.Places.All().FirstOrDefault(p => p.Id == placeId);
            if (place == null)
            {
                return NotFound();
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.data.Users.All().FirstOrDefault(u => u.Id == userId);
            if (user.Favourites == null)
            {
                user.Favourites = new HashSet<Place>();
            }

            user.Favourites.Add(place);
            this.data.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult RemoveFavourite(int placeId)
        {
            var place = this.data.Places.All().FirstOrDefault(p => p.Id == placeId);
            if (place == null)
            {
                return NotFound();
            }

            var userId = this.User.Identity.GetUserId();
            var user = this.data.Users.All().FirstOrDefault(u => u.Id == userId);
            user.Favourites.Remove(place);
            this.data.SaveChanges();
            return Ok();
        }
    }
}
