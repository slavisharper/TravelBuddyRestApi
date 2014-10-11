namespace App.WebApi.Controllers
{
    using App.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using App.WebApi.Models;
    using App.Models;

    [Authorize]
    public class TravelsController : BaseController
    {
        public TravelsController(ITravelBuddyData data)
            :base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            string id = this.User.Identity.GetUserId();
            var result = this.data.Travels
                .All()
                .Where(t => t.User.Id == id)
                .Select(SimpleTravelModel.FromTravel);

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GetLast(int count)
        {
            string id = this.User.Identity.GetUserId();
            var result = this.data.Travels
                .All()
                .OrderByDescending(t => t.StartDate)
                .Where(t => t.User.Id == id)
                .Take(count)
                .Select(SimpleTravelModel.FromTravel);

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            string userId = this.User.Identity.GetUserId();
            var result = this.data.Travels
                .All()
                .FirstOrDefault(t => t.Id == id);

            if (result.UserId != userId)
            {
                return BadRequest("This trip is not yours!");
            }

            var model = new TravelDetailsModel(result);
            return Ok(model);
        }

        [HttpPost]
        public IHttpActionResult Create(CreateTravelModel newTravel)
        {
            string userId = this.User.Identity.GetUserId();

            var dbTravel = new Travel
            {
                Distance = 0,
                StartDate = DateTime.Now,
                Title = newTravel.Title,
                UserId = userId
            };

            this.data.Travels.Add(dbTravel);
            this.data.SaveChanges();

            var travelInfo = new SimpleTravelModel
            {
                Description = dbTravel.Description,
                Name = dbTravel.Title,
                StartDate = dbTravel.StartDate,
                Id = dbTravel.Id,
                Distance = dbTravel.Distance
            };

            return Ok(travelInfo);
        }

        [HttpPut]
        public IHttpActionResult Change(int id, ModifiedTravelModel changedModel)
        {
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Remove(int id, SimpleTravelModel travelToRemove)
        {
            return Ok();
        }
    }
}
