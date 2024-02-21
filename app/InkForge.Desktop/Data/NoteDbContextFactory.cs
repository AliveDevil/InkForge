using InkForge.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using SmartFormat;
using InkForge.Desktop.Data.Options;

namespace InkForge.Desktop.Data;

public class NoteDbContextFactory(LocalWorkspaceOptions options, IConfiguration configuration) : IDbContextFactory<NoteDbContext>
{
	private string? _connectionString;

	public NoteDbContext CreateDbContext()
	{
		_connectionString ??= Smart.Format(configuration.GetConnectionString("DefaultConnection")!, new
		{
			WorkspaceFile = options.DbPath
		});

		DbContextOptionsBuilder<NoteDbContext> builder = new();
		builder.UseSqlite(_connectionString, o => o.MigrationsAssembly("InkForge.Sqlite"));

		return new NoteDbContext(builder.Options);
	}
}
