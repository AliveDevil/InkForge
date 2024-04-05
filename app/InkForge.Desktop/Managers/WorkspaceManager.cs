using InkForge.Data;
using InkForge.Desktop.Data.Options;
using InkForge.Desktop.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

namespace InkForge.Desktop.Managers;

public class WorkspaceManager(IServiceProvider serviceProvider) : ReactiveObject
{
	private readonly IServiceProvider _serviceProvider = serviceProvider;
	private Workspace? _workspace;

	public Workspace? Workspace
	{
		get => _workspace;
		private set => this.RaiseAndSetIfChanged(ref _workspace, value);
	}

	public ValueTask CloseWorkspace()
	{
		_workspace?.Dispose();
		Workspace = null;
		return ValueTask.CompletedTask;
	}

	public async ValueTask OpenWorkspace(string path, bool createFile = false)
	{
		await CloseWorkspace().ConfigureAwait(false);
		if (await CreateLocalWorkspace(path, createFile).ConfigureAwait(false) is { } workspace)
		{
			Workspace = workspace;
		}
	}

	private async ValueTask<Workspace?> CreateLocalWorkspace(string path, bool createFile)
	{
		FileInfo file = new(path);
		if (!(createFile || file.Exists))
		{
			return null;
		}

		file.Directory!.Create();
		IServiceScope? scope = null;
		IWorkspaceContext workspaceContext;
		try
		{
			scope = _serviceProvider.CreateScope();
			var serviceProvider = scope.ServiceProvider;
			var options = serviceProvider.GetRequiredService<LocalWorkspaceOptions>();
			options.DbPath = path;

			workspaceContext = serviceProvider.GetRequiredService<IWorkspaceContext>();
			workspaceContext.Workspace = new Workspace(scope)
			{
				Name = Path.GetFileNameWithoutExtension(file.Name),
				Options = options,
			};

			var dbFactory = serviceProvider.GetRequiredService<IDbContextFactory<NoteDbContext>>();
			await using var dbContext = await dbFactory.CreateDbContextAsync().ConfigureAwait(false);
			var db = dbContext.Database;
			if ((await db.GetPendingMigrationsAsync().ConfigureAwait(false)).Any())
			{
				if (file.Exists)
				{
					file.CopyTo(Path.ChangeExtension(file.FullName, $"{DateTime.Now:s}{file.Extension}"));
				}

				await db.MigrateAsync().ConfigureAwait(false);
			}

			scope = null;
		}
		catch (Exception)
		{
			// Show Error through TopLevels.ActiveTopLevel
			return null;
		}
		finally
		{
			scope?.Dispose();
		}

		return workspaceContext.Workspace;
	}
}
