using Microsoft.EntityFrameworkCore;
using storehouse_backend.Models;

namespace storehouse_backend.Context
{
    public class StoreHouseDBContext: DbContext
    {
        public StoreHouseDBContext(DbContextOptions<StoreHouseDBContext> options) : base(options) { }

        DbSet<Product> products { get; set; }
    }
}
