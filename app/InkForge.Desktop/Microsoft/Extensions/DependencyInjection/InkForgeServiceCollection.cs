using InkForge.Data;
using InkForge.Desktop.Data;
using InkForge.Desktop.Data.Options;
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

		services.AddScoped<IWorkspaceAccessor, WorkspaceAccessor>();
		services.AddScoped<IDbContextFactory<NoteDbContext>, NoteDbContextFactory>();

		services.AddScoped<LocalWorkspaceOptions>();

		services.AddSingleton<LandingViewModel>();
		services.AddSingleton<WorkspaceManager>();

		Locator.CurrentMutable.RegisterViewsForViewModels(typeof(InkForgeServiceCollections).Assembly);

		return services;
	}
}
