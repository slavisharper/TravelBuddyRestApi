using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.WebApi.Models
{
    public class PlaceDetailModel
    {
        public PlaceDetailModel(bool isPublic, Place place, string userId = null)
        {
            this.Id = place.Id;
            this.Description = place.Description;
            this.Country = place.Country;
            this.LastVisited = place.LastVisited;
            this.Latitude = place.Latitude;
            this.Longtitude = place.Longtitude;
            this.Title = place.Title;
            this.Photos = GetImages(isPublic, place, userId);
            this.Visitors = GetNames(place);
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public double Longtitude { get; set; }

        public double Latitude { get; set; }

        public DateTime LastVisited { get; set; }

        public ICollection<String> Visitors { get; set; }

        public ICollection<int> Photos { get; set; }

        private ICollection<string> GetNames(Place place)
        {
            ICollection<string> names = place.Visitors.Select(u => u.UserName).ToList();
            return names;
        }

        private ICollection<int> GetImages(bool isPublic, Place place, string userId)
        {
            if (isPublic)
            {
                var result = place.Photos
                    .Where(p => p.PlaceId == place.Id)
                    .Take(10)
                    .Select(p => p.Id)
                    .ToList();
                return result;
            }
            else
            {
                var result = place.Photos
                    .Where(p => p.PlaceId == place.Id && p.UserId == userId)
                    .Select(p => p.Id)
                    .ToList();
                return result;
            }
        }
    }
}
