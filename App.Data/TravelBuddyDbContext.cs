namespace App.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using App.Data.Migrations;
    using App.Models;

    public class TravelBuddyDbContext : IdentityDbContext<ApplicationUser>
    {
        public TravelBuddyDbContext()
            : base("TravelBuddyDatabase", throwIfV1Schema: false)
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<TravelBuddyDbContext, Configuration>());
        }

        public static TravelBuddyDbContext Create()
        {
            return new TravelBuddyDbContext();
        }

        public IDbSet<Place> Places { get; set; }

        public IDbSet<Photo> Photos { get; set; }

        public IDbSet<Travel> Travels { get; set; }
    }
}
