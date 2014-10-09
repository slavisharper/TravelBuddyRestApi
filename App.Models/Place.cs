using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Models
{
    public class Place
    {
        private ICollection<Photo> photos;
        private ICollection<Travel> travels;
        private ICollection<ApplicationUser> visitors;

        public Place()
        {
            this.photos = new HashSet<Photo>();
            this.visitors = new HashSet<ApplicationUser>();
            this.travels = new HashSet<Travel>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public double Longtitude { get; set; }

        public double Latitude { get; set; }

        public DateTime LastVisited { get; set; }

        public virtual ICollection<Travel> Travels
        {
            get { return this.travels; }
            set { this.travels = value; }
        }

        public virtual ICollection<ApplicationUser> Visitors
        {
            get { return this.visitors; }
            set { this.visitors = value; }
        }

        public virtual ICollection<Photo> Photos
        {
            get { return this.photos; }
            set { this.photos = value; }
        }
    }
}
