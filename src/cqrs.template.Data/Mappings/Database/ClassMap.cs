using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace cqrs.template.Data.Mappings.Database
{
	public class ClassMap
		// : IEntityTypeConfiguration<SomeEntity>
	{
		// don't forget to change the name of this class for YourEntityMap : UserMap
		// uncomment line 7 and add your entity between the <>
		public void Configure(EntityTypeBuilder<ClassMap> builder)
		{
			// your database configuration comes here
		}
	}
}