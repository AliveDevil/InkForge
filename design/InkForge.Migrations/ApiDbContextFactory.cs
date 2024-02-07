using InkForge.Api.Data;

using Microsoft.EntityFrameworkCore;

namespace InkForge.Migrations;

public class ApiDbContextFactory : MigratingDbContextFactory<ApiDbcontext>
{
	protected override void Configure(
		DbContextOptionsBuilder<ApiDbcontext> optionsBuilder,
		string connectionString,
		string provider
	) => _ = provider switch
	{
		"Sqlite" => optionsBuilder.UseSqlite(connectionString,
			m => m.MigrationsAssembly("InkForge.Api.Sqlite")
		),

		_ => throw new Exception($"Invalid DbProvider: {provider}")
	};

	protected override ApiDbcontext CreateDbContext(DbContextOptions<ApiDbcontext> options) => new(options);
}
