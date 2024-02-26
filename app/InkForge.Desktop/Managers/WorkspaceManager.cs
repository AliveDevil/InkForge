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

	public Task CloseWorkspace()
	{
		_workspace?.Dispose();
		Workspace = null;
		return Task.CompletedTask;
	}

	public async Task OpenWorkspace(string path, bool createFile = false)
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
			await using (var dbContext = dbFactory.CreateDbContext())
			{
				var db = dbContext.Database;
				await using var transaction = await db.BeginTransactionAsync().ConfigureAwait(false);
				try
				{
					await db.MigrateAsync().ConfigureAwait(false);
				}
				catch
				{
					// Show Error through TopLevels.ActiveTopLevel
					await transaction.RollbackAsync().ConfigureAwait(false);
					return null;
				}

				await transaction.CommitAsync().ConfigureAwait(false);
			}

			scope = null;
		}
		finally
		{
			scope?.Dispose();
		}

		return workspaceContext.Workspace;
	}
}
