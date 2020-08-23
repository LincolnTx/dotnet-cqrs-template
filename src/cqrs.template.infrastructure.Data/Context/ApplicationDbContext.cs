using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace cqrs.template.infrastructure.Data.Context
{
	public class ApplicationDbContext : DbContext
	{
		// your db context here
		// sample: public DbSet<User> User { get; set; }

		private IDbContextTransaction _currentTransaction;
		public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
		public bool HasActiveTransaction => _currentTransaction != null;
		private readonly IMediator _mediator;

		public ApplicationDbContext()
		{
		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}

		// protected override void OnConfiguring(DbContextOptionsBuilder options) =>
		// 	options.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString"),
		// 		npgsqlOptionsAction: pgOptions =>
		// 		{
		// 			pgOptions.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
		// 		}
		// );

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// sample: modelBuilder.ApplyConfiguration(new UserMap());

		}

		public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			// Dispatch Domain Events collection. 
			// Choices:
			// A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
			// side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
			// B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
			// You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
			await _mediator.DispatchDomainEventsAsync(this);

			// After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
			// performed through the DbContext will be committed
			return await base.SaveChangesAsync(cancellationToken) > 0;
		}
	}
}