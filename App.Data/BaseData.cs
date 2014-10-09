namespace App.Data
{
    using System;
    using System.Collections.Generic;
    
    using App.Data.Repositories;

    public abstract class BaseData
    {
        private TravelBuddyDbContext context;
        private IDictionary<Type, object> repositories;

        public BaseData(TravelBuddyDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        protected IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(Repository<T>);

                if (!this.repositories.ContainsKey(type))
                {
                    var newRepository = Activator.CreateInstance(typeof(Repository<T>), this.context);
                    this.repositories.Add(type, newRepository);
                }

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeOfModel];
        }
    }
}
