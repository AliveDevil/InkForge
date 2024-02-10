using InkForge.Common.Controllers;
using InkForge.Common.Data;
using InkForge.Common.ViewModels;
using InkForge.Common.ViewModels.Landing;
using InkForge.Data;

using ReactiveUI;

using Splat;

namespace Microsoft.Extensions.DependencyInjection;

public static class InkForgeServiceCollections
{
	public static IServiceCollection AddInkForge(this IServiceCollection services)
	{
		services.AddHttpClient();

		services.AddDbContextFactory<NoteDbContext, NoteDbContextFactory>();

		services.AddSingleton<LandingViewModel>();
		services.AddSingleton<LandingViewModelFactory>();
		services.AddSingleton<WorkspaceController>();

		Locator.CurrentMutable.RegisterViewsForViewModels(typeof(InkForgeServiceCollections).Assembly);

		return services;
	}
}
