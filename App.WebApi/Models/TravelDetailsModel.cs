using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.WebApi.Models
{
    public class TravelDetailsModel
    {
        public TravelDetailsModel(Travel travel)
        {
            this.Id = travel.Id;
            this.Title = travel.Title;
            this.User = travel.User.Email;
            this.UserId = travel.UserId;
            this.Distance = travel.Distance;
            this.StartDate = travel.StartDate;
            this.EndDate = travel.EndDate;
            this.Description = travel.Description;
            this.Photos = travel.Photos.AsQueryable()
                .Select(PhotoModel.FromPhoto).ToList();
            this.Places = travel.Places.AsQueryable()
                .Select(PlaceModel.FromPlace).ToList();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Distance { get; set; }

        public string UserId { get; set; }

        public virtual String User { get; set; }

        public virtual ICollection<PhotoModel> Photos { get; set; }

        public virtual ICollection<PlaceModel> Places { get; set; }
    }
}