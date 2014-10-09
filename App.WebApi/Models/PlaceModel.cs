using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace App.WebApi.Models
{
    public class PlaceModel
    {
        public static Expression<Func<Place, PlaceModel>> FromPlace
        {
            get
            {
                return p => new PlaceModel
                {
                    Id = p.Id,
                    Country = p.Country,
                    LastVisited = p.LastVisited,
                    Latitude = p.Latitude,
                    Longtitude = p.Longtitude,
                    Title = p.Title,
                    Description = p.Description
                };
            }
        }

        public int? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public double Longtitude { get; set; }

        public double Latitude { get; set; }

        public DateTime LastVisited { get; set; }
    }
}
