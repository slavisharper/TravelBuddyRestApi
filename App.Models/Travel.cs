using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Models
{
    public class Travel
    {
        private ICollection<Place> places;
        private ICollection<Photo> photos;

        public Travel()
        {
            this.places = new HashSet<Place>();
            this.photos = new HashSet<Photo>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Distance { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Photo> Photos 
        {
            get { return this.photos; }
            set { this.photos = value; }
        }

        public virtual ICollection<Place> Places
        {
            get { return this.places; }
            set { this.places = value; }
        }
    }
}
