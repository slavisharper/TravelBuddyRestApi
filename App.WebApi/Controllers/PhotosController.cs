namespace App.WebApi.Controllers
{
    using App.Data;
    using App.Models;
    using App.WebApi.Models;
    using Microsoft.AspNet.Identity;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    public class PhotosController : BaseController
    {
        public PhotosController(ITravelBuddyData data)
            :base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult AllByUser()
        {
            string userId = this.User.Identity.GetUserId();
            var result = this.data.Photos
                .All()
                .Where(p => p.UserId == userId)
                .Select(PhotoModel.FromPhoto);

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult AllByPlaceId(int placeId)
        {
            string userId = this.User.Identity.GetUserId();
            var result = this.data.Photos
                .All()
                .Where(p => p.PlaceId == placeId)
                .Select(PhotoModel.FromPhoto);

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Add(PhotoModel newPhoto)
        {
            string userId = this.User.Identity.GetUserId();
            var dbPhoto = new Photo
            {
                Image = newPhoto.Image,
                Name = newPhoto.Name,
                PlaceId = newPhoto.PlaceID,
                UserId = userId
            };

            this.data.Photos.Add(dbPhoto);
            this.data.SaveChanges();

            newPhoto.Id = dbPhoto.Id;
            newPhoto.UserId = dbPhoto.UserId;

            return Ok(newPhoto);
        }
    }
}
