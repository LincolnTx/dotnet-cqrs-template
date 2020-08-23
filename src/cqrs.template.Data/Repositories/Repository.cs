using cqrs.template.Data.Context;
using cqrs.template.domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace cqrs.template.Data.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
	{
		protected readonly ApplicationDbContext _applicationDbContext;
		protected readonly DbSet<TEntity> _dbSet;

		public Repository( ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		public void Add(TEntity obj)
		{
			_applicationDbContext.Add(obj);
		}
	}
}