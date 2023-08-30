using API_propia.Data_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace API_propia.DataAccess
{
    public class HotelDBContext : DbContext
    {
        public HotelDBContext(DbContextOptions<HotelDBContext> options) : base(options) 
        {

        }

        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
