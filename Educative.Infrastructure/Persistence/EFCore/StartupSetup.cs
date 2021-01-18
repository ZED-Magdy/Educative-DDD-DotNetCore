using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Educative.Infrastructure.Persistence.EFCore
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration config) =>
           services.AddDbContext<DBContext>(opt =>
                opt.UseMySql(config.GetConnectionString("DefaultConnection"))
            );
    }
}
