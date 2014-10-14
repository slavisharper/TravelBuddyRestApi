using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PlaceId { get; set; }

        public virtual Place Place { get; set; }
    }
}
