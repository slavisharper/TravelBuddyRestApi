namespace App.Data
{
    using App.Data.Repositories;
    using App.Models;

    public interface ITravelBuddyData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Place> Places { get; }

        IRepository<Travel> Travels { get; }

        IRepository<Photo> Photos { get; }

        void SaveChanges();
    }
}
