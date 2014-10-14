using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace App.WebApi.Models
{
    public class PhotoModel
    {
        public static Expression<Func<Photo, PhotoModel>> FromPhoto
        {
            get
            {
                return p => new PhotoModel
                {
                    Id = p.Id,
                    Image = p.Image,
                    Name = p.Name,
                    PlaceID = p.PlaceId,
                    UserId = p.UserId
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string UserId { get; set; }

        public int PlaceID { get; set; }
    }
}
