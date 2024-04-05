using Avalonia.Controls.Templates;

using Dock.Model.Core;

using InkForge.Data;
using InkForge.Desktop;
using InkForge.Desktop.Data;
using InkForge.Desktop.Data.Options;
using InkForge.Desktop.Managers;
using InkForge.Desktop.Models;
using InkForge.Desktop.ViewModels.Workspaces;
using InkForge.Desktop.ViewModels.Workspaces.Internal;

using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class InkForgeServiceCollections
{
	public static IServiceCollection AddInkForge(this IServiceCollection services)
	{
		services.AddHttpClient();

		// Singletons
		// - Concrete
		services.AddSingleton<DocumentManager>();
		services.AddSingleton<InkForgeFactory>();
		services.AddSingleton<WorkspaceManager>();

		// - Service
		services.AddSingleton<IDataTemplate, AppViewLocator>();
		services.AddSingleton<IWorkspaceViewModelFactory, WorkspaceViewModelFactory>();

		// Scoped
		// - Concrete
		services.AddScoped<LocalWorkspaceOptions>();
		services.AddScoped<NoteStore>();

		// - Service
		services.AddScoped<IDbContextFactory<NoteDbContext>, NoteDbContextFactory>();
		services.AddScoped<IWorkspaceContext, WorkspaceContext>();

		// - Forwarders
		services.AddScoped(s => s.GetRequiredService<IWorkspaceContext>().Workspace!);

		return services;
	}
}
