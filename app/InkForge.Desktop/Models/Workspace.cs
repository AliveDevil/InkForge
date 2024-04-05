using InkForge.Desktop.Data.Options;

using Microsoft.Extensions.DependencyInjection;

namespace InkForge.Desktop.Models;

public sealed class Workspace : IDisposable
{
	private bool _disposedValue;
	private IServiceScope? _scope;

	public string Name { get; set; } = default!;

	public LocalWorkspaceOptions Options { get; set; } = default!;

	public IServiceProvider Services => _scope!.ServiceProvider;

	public Workspace(IServiceScope scope)
	{
		_scope = scope;
	}

	public void Dispose()
	{
		if (!_disposedValue)
		{
			if (_scope is { })
			{
				_scope.Dispose();
				_scope = null;
			}

			_disposedValue = true;
		}
	}

	// private async Task LoadNotes()
	// {
	// 	await using var dbContext = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
	// 	await foreach (var asdf in dbContext.Notes.AsAsyncEnumerable().ConfigureAwait(false))
	// 	{

	// 	}
	// 	_ = (await dbContext.Notes.ToListAsync().ConfigureAwait(false));
	// }
}
