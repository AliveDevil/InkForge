using InkForge.Desktop.Controllers;
using InkForge.Desktop.Data;
using InkForge.Desktop.Services;
using InkForge.Desktop.ViewModels;
using InkForge.Data;

using Microsoft.EntityFrameworkCore;

using ReactiveUI;

using Splat;

namespace Microsoft.Extensions.DependencyInjection;

public static class InkForgeServiceCollections
{
	public static IServiceCollection AddInkForge(this IServiceCollection services)
	{
		services.AddHttpClient();

		services.AddScoped<IDbContextFactory<NoteDbContext>, NoteDbContextFactory>();
		services.AddScoped(s => s.GetRequiredService<IDbContextFactory<NoteDbContext>>().CreateDbContext());

		services.AddScoped<WorkspaceContext>();

		services.AddSingleton<LandingViewModel>();
		services.AddSingleton<WorkspaceController>();

		Locator.CurrentMutable.RegisterViewsForViewModels(typeof(InkForgeServiceCollections).Assembly);

		return services;
	}
}
