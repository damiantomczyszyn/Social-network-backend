using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Model;
using Microsoft.Extensions.Configuration;

namespace SocialNetwork.Data
{
    public class DefaultContext : BaseContext
    {
        public IConfiguration Configuration { get; }

        public DefaultContext(IConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        public DefaultContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = Configuration.GetConnectionString("Database");
                optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }
    }
}
