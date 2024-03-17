using InkForge.Data;
using InkForge.Desktop.Data.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InkForge.Desktop.Models;

public sealed class Workspace : IDisposable
{
	private readonly IDbContextFactory<NoteDbContext> _dbContextFactory;
	private bool _disposedValue;
	private IServiceScope? _scope;

	public string Name { get; set; } = default!;

	public LocalWorkspaceOptions Options { get; set; } = default!;

	public IServiceProvider Services => _scope!.ServiceProvider;

	public Workspace(IServiceScope scope)
	{
		_scope = scope;
		_dbContextFactory = Services.GetRequiredService<IDbContextFactory<NoteDbContext>>();
	}

	// public Note AddNote(Note? parent)
	// {
	// }

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
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
}
