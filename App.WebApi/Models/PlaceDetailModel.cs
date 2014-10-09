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
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public double Longtitude { get; set; }

        public double Latitude { get; set; }

        public DateTime LastVisited { get; set; }

        public ICollection<String> Visitors { get; set; }

        public ICollection<Byte[]> Photos { get; set; }

        private ICollection<string> GetNames(ICollection<ApplicationUser> users)
        {
            ICollection<string> names = users.Select(u => u.UserName).ToList();
            return names;
        }

        private ICollection<byte[]> GetImages(bool isPublic, Place place, string userId)
        {
            if (isPublic)
            {
                var result = place.Photos
                    .Where(p => p.PlaceId == place.Id)
                    .Select(p => p.Image)
                    .ToList();
                return result;
            }
            else
            {
                var result = place.Photos
                    .Where(p => p.PlaceId == place.Id && p.UserId == userId)
                    .Select(p => p.Image)
                    .ToList();
                return result;
            }
        }
    }
}
