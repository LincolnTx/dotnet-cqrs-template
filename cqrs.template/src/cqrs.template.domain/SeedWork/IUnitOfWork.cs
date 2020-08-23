using System;
using System.Threading.Tasks;

namespace cqrs.template.domain.SeedWork
{
	public interface IUnitOfWork
	{
		Task<bool> Commit();
	}
}