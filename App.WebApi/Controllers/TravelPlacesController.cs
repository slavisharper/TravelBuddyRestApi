using App.Data;
using App.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        public IHttpActionResult AddPlace(int id, PlaceModel newPlace)
        {
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult RemovePlace(int id, PlaceModel placeToRemove)
        {
            return Ok();
        }
    }
}
