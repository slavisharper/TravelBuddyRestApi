using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace App.WebApi.Models
{
    public class SimpleTravelModel
    {
        public static Expression<Func<Travel, SimpleTravelModel>> FromGame
        {
            get
            {
                return t => new SimpleTravelModel
                {
                    Id = t.Id,
                    Name = t.Title,
                    EndDate = t.EndDate,
                    StartDate = t.StartDate,
                    Distance = t.Distance,
                    Description = t.Description
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Distance { get; set; }
    }
}