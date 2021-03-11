using System.Data.Entity;
using System.Security.AccessControl;
using CinemaC.Models.Domain;

namespace CinemaC.DataBaseLayer
{
    public class CinemaContext :DbContext
    {
        public CinemaContext(): base ("Cinema")
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        
        public DbSet<TimeSlot>TimeSlots { get; set; }

        public DbSet<Hall> Halls { get;  set; }


    }
}