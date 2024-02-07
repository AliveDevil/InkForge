using InkForge.Data;

using Microsoft.EntityFrameworkCore;

namespace InkForge.Migrations;

public class NoteDbContextFactory : MigratingDbContextFactory<NoteDbContext>
{
	protected override void Configure(
		DbContextOptionsBuilder<NoteDbContext> optionsBuilder,
		string connectionString,
		string provider
	) => _ = provider switch
	{
		"Sqlite" => optionsBuilder.UseSqlite(connectionString,
			m => m.MigrationsAssembly("InkForge.Sqlite")
		),

		_ => throw new Exception($"Invalid DbProvider: {provider}")
	};

	protected override NoteDbContext CreateDbContext(DbContextOptions<NoteDbContext> options) => new(options);
}
