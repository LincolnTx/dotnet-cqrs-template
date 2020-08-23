using System.Threading.Tasks;
using cqrs.template.infrastructure.Data.Context;
using cqrs.template.domain.SeedWork;

namespace cqrs.template.infrastructure.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public UnitOfWork(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public async Task<bool> Commit()
		{
			return await _applicationDbContext.SaveEntitiesAsync();
		}

		public void Dispose()
		{
			_applicationDbContext.Dispose();
		}
	}
}