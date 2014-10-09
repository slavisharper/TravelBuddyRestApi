using App.Data;
using App.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace App.WebApi.Controllers
{
    [Authorize]
    public class TravelPlacesController : BaseController
    {
        public TravelPlacesController(ITravelBuddyData data)
            :base(data)
        {
        }

        // api/travels/{id}/places"
        [HttpPut]
        public IHttpActionResult AddPlace(int id, [FromUri]int placeId)
        {
            var place = this.data.Places.All().FirstOrDefault(p => p.Id == placeId);
            var travel = this.data.Travels.All().FirstOrDefault(t => t.Id == id);
            var userId = this.User.Identity.GetUserId();

            if (place == null || travel == null)
            {
                return NotFound();
            }

            if (userId != travel.UserId)
            {
                return BadRequest("Not you travel!");
            }

            var user = this.data.Users.All().FirstOrDefault(u => u.Id == userId);
            place.Visitors.Add(user);
            place.LastVisited = DateTime.Now;
            // place.Travels.Add(travel);
            travel.Places.Add(place);
            return Ok(new { 
                TravelTitle = travel.Title,
                PlaceTitle = place.Title
            });
        }

        [HttpDelete]
        public IHttpActionResult RemovePlace(int id, [FromUri]int placeId)
        {
            var place = this.data.Places.All().FirstOrDefault(p => p.Id == placeId);
            var travel = this.data.Travels.All().FirstOrDefault(t => t.Id == id);
            var userId = this.User.Identity.GetUserId();

            if (place == null || travel == null)
            {
                return NotFound();
            }

            if (userId != travel.UserId)
            {
                return BadRequest("Not you travel!");
            }

            travel.Places.Remove(place);
            return Ok();
        }
    }
}
