using Microsoft.EntityFrameworkCore;
using NS.Core.Data;
using NS.Core.DomainObjects;
using NS.Core.Mediator;
using NS.Customers.API.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NS.Customers.API.Data
{
    public class CustomersContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomersContext(DbContextOptions<CustomersContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            foreach (var foreignKeys in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKeys.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomersContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var success = await base.SaveChangesAsync() > 0;

            if (success)
            {
                await _mediatorHandler.PublishEvents(this);
            }

            return success;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublishEvents<T>(this IMediatorHandler mediatorHandler, T context) where T : DbContext
        {
            var domainEntities = context.ChangeTracker.Entries<Entity>().Where(e => e.Entity.Events.Any()).ToList();

            var domainEvents = domainEntities.SelectMany(e => e.Entity.Events).ToList();

            domainEntities.ForEach(e => e.Entity.ClearEvents());

            var tasks = domainEvents.Select(async (domainEvent) => { await mediatorHandler.PublishEvent(domainEvent); });

            await Task.WhenAll(tasks);
        }
    }
}