using InkForge.Data;
using InkForge.Desktop.Models;
using InkForge.Desktop.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

namespace InkForge.Desktop.Controllers;

public class WorkspaceController : ReactiveObject
{
	private readonly IServiceProvider _serviceProvider;
	private Workspace _workspace;

	public Workspace Workspace
	{
		get => _workspace;
		set => this.RaiseAndSetIfChanged(ref _workspace, value);
	}

	public WorkspaceController(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;
	}

	public async Task OpenWorkspace(string path, bool createFile = false)
	{
		if (await CreateWorkspace(path, createFile) is { } workspace)
		{
			Workspace = workspace;
		}
	}

	private async ValueTask<Workspace?> CreateWorkspace(string path, bool createFile)
	{
		FileInfo file = new(path);
		if (!(createFile || file.Exists))
		{
			return null;
		}

		file.Directory!.Create();
		var scope = _serviceProvider.CreateScope();
		var scopeServiceProvider = scope.ServiceProvider;
		var context = scope.ServiceProvider.GetRequiredService<WorkspaceContext>();
		context.DbPath = path;

		var db = scopeServiceProvider.GetRequiredService<NoteDbContext>();
		await using (var transaction = await db.Database.BeginTransactionAsync().ConfigureAwait(false))
		{
			try
			{
				await db.Database.MigrateAsync().ConfigureAwait(false);
			}
			catch
			{
				await transaction.RollbackAsync().ConfigureAwait(false);
				return null;
			}

			await transaction.CommitAsync().ConfigureAwait(false);
		}

		return new(scope);
	}
}
