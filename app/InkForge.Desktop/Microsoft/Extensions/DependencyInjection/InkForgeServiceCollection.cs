using InkForge.Data;
using InkForge.Desktop.Data;
using InkForge.Desktop.Data.Options;
using InkForge.Desktop.Dock;
using InkForge.Desktop.Managers;
using InkForge.Desktop.Models;
using InkForge.Desktop.ViewModels;

using Microsoft.EntityFrameworkCore;

using ReactiveUI;

using Splat;

namespace Microsoft.Extensions.DependencyInjection;

public static class InkForgeServiceCollections
{
	public static IServiceCollection AddInkForge(this IServiceCollection services)
	{
		services.AddHttpClient();

		// Singletons
		// - Concrete
		services.AddSingleton<DocumentManager>();
		services.AddSingleton<WorkspaceFactory>();
		services.AddSingleton<WorkspaceManager>();
		services.AddSingleton<WorkspacesViewModel>();

		// Scoped
		// - Concrete
		services.AddScoped<LocalWorkspaceOptions>();

		// - Service
		services.AddScoped<IDbContextFactory<NoteDbContext>, NoteDbContextFactory>();
		services.AddScoped<IWorkspaceContext, WorkspaceContext>();

		// - Forwarders
		services.AddScoped(s => s.GetRequiredService<IWorkspaceContext>().Workspace!);

		Locator.CurrentMutable.RegisterViewsForViewModels(typeof(InkForgeServiceCollections).Assembly);

		return services;
	}
}
