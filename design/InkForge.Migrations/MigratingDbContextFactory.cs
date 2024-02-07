using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InkForge.Migrations;

public abstract class MigratingDbContextFactory<T> : IDesignTimeDbContextFactory<T>
	where T : DbContext
{
	public T CreateDbContext(string[] args)
	{
		var configuration = new ConfigurationBuilder()
			.AddCommandLine(args)
			.Build();

		var options = new DbContextOptionsBuilder<T>();
		switch (configuration.GetValue<string>("DbProvider"))
		{
			case null:
				throw new Exception("DbProvider not set.");

			case { } provider:
				Configure(options, configuration.GetConnectionString("DefaultConnection")!, provider);
				break;
		}

		return CreateDbContext(options.Options);
	}

	protected abstract void Configure(DbContextOptionsBuilder<T> optionsBuilder, string connectionString, string provider);

	protected abstract T CreateDbContext(DbContextOptions<T> options);
}
