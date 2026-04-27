using Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Data
{
    public class HotelContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public HotelContext (DbContextOptions<HotelContext> options) : base(options)
        {

        }
    }
}
