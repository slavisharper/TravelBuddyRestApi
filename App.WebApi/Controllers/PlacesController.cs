namespace App.WebApi.Controllers
{
    using App.Data;
    using App.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using App.WebApi.Models;

    public class PlacesController : BaseController
    {
        private const int PageSize = 10;
        private const int EarthRadius = 6378137;

        public PlacesController(ITravelBuddyData data)
            :base(data)
        {
        }

        // api/places/
        [HttpGet]
        public IHttpActionResult All()
        {
            return this.AllByPage(0, "asc", "top");
        }

        // api/places/{id}
        [HttpGet]
        public IHttpActionResult ById(int id)
        {
            return this.ById(id, true);
        }

        // api/places/{id}?isPublic=true
        [HttpGet]
        public IHttpActionResult ById(int id, bool isPublic)
        {
            string userId = this.User.Identity.GetUserId();
            var place = this.data.Places.All().FirstOrDefault(p => p.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            var placeDetails = new PlaceDetailModel(isPublic, place, userId);
            return Ok(placeDetails);
        }

        [HttpGet]
        // "last", "top", "nearby"
        public IHttpActionResult All(int page, string orderBy)
        {
            return this.AllByPage(page, "asc", orderBy);
        }

        [HttpGet]
        public IHttpActionResult AllByPage(int page, string orderType, string orderBy)
        {
            var result = this.data.Places.All().AsQueryable();
            bool isAscending = orderType == "asc";

            if (orderBy == "last")
            {
                result = GetPlacesByVisitedDate(page, isAscending);
            }
            else if (orderBy == "top")
            {
                result = GetPlacesByVisitorsCount(page, isAscending);
            }
            else if (orderBy == "nearby")
            {
                string userId = this.User.Identity.GetUserId();
                var user = this.data.Users.All().FirstOrDefault(u => u.Id == userId);
                if (user.Latitude == null || user.Longtitude == null)
                {
                    return BadRequest("Update user location");
                }

                result = GetNearbyPlaces(page, user);
            }

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult Add(PlaceModel newPlace)
        {
            var dbPlace = new Place
            {
                Description = newPlace.Description,
                LastVisited  = DateTime.Now,
                Latitude = newPlace.Latitude,
                Longtitude = newPlace.Longtitude,
                Title = newPlace.Title,
                Country = newPlace.Country
            };

            var userId = this.User.Identity.GetUserId();
            var user = this.data.Users.All().FirstOrDefault(u => u.Id == userId);

            dbPlace.Visitors.Add(user);
            this.data.Places.Add(dbPlace);
            this.data.SaveChanges();

            newPlace.LastVisited = dbPlace.LastVisited;
            newPlace.Id = dbPlace.Id;
            return Ok(newPlace);
        }

        private IQueryable<Place> GetNearbyPlaces(int page, ApplicationUser user)
        {
            var result = this.data.Places
                    .All()
                    .Where(p => GetDistance(p.Latitude, p.Longtitude, user.Latitude, user.Longtitude) <= 100.0)
                    .AsQueryable()
                    .OrderBy(p => p.Visitors.Count)
                    .Skip(page * PageSize)
                    .Take(PageSize);

            return result;
        }

        private IQueryable<Place> GetPlacesByVisitorsCount(int page, bool isAscending)
        {
            if (isAscending)
            {
                var result = this.data.Places
                    .All()
                    .OrderBy(p => p.Visitors.Count)
                    .Skip(page * PageSize)
                    .Take(PageSize);

                return result;
            }
            else
            {
                var result = this.data.Places
                    .All()
                    .OrderByDescending(p => p.Visitors.Count)
                    .Skip(page * PageSize)
                    .Take(PageSize);

                return result;
            }
        }

        private IQueryable<Place> GetPlacesByVisitedDate(int page, bool isAscending)
        {
            if (isAscending)
	        {
                var result = this.data.Places
                    .All()
                    .OrderBy(p => p.LastVisited)
                    .Skip(page * PageSize)
                    .Take(PageSize);

                return result;
	        }
            else
            {
                var result = this.data.Places
                    .All()
                    .OrderByDescending(p => p.LastVisited)
                    .Skip(page * PageSize)
                    .Take(PageSize);

                return result;
            }
        }

        private double GetDistance(double placeLat, double placeLong, double? userLat, double? userLong)
        {
            double userLatitude = (double)userLat;
            double userLongtitude = (double)userLong;
            var dLat = (userLatitude - placeLat) * Math.PI / 180;
            var dLng = (userLongtitude - placeLong) * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(placeLat * Math.PI / 180) *
                Math.Cos(userLatitude * Math.PI / 180) *
                Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = EarthRadius * c;
            return Math.Round(d);
        }
    }
}
