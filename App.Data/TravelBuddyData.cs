namespace App.Data
{
    using System;
    using System.Collections.Generic;

    using App.Data.Repositories;
    using App.Models;

    public class TravelBuddyData : BaseData, ITravelBuddyData
    {
        public TravelBuddyData()
            :this(new TravelBuddyDbContext())
        {
        }
        public TravelBuddyData(TravelBuddyDbContext context)
            :base(context)
        {
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Place> Places
        {
            get { return this.GetRepository<Place>(); }
        }

        public IRepository<Travel> Travels
        {
            get { return this.GetRepository<Travel>(); }
        }

        public IRepository<Photo> Photos
        {
            get { return this.GetRepository<Photo>(); }
        }
    }
}
