using Microsoft.EntityFrameworkCore;

namespace OrmAirways.Data
{
    public class DbInitializer
    {
        public static void Initialize(AirwaysDbContext context)
        {
            context.Database.Migrate();

            
        }
    }
}
