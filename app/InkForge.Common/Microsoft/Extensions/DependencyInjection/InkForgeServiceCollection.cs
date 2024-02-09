using InkForge.Common.Controllers;
using InkForge.Common.Data;
using InkForge.Common.ViewModels;
using InkForge.Common.Views;
using InkForge.Data;

using ReactiveUI;

namespace Microsoft.Extensions.DependencyInjection;

public static class InkForgeServiceCollections
{
	public static IServiceCollection AddInkForge(this IServiceCollection services)
	{
		services.AddHttpClient();

		services.AddDbContextFactory<NoteDbContext, NoteDbContextFactory>();

		services.AddSingleton<WorkspaceController>();

		services.AddTransient<IViewFor<LandingViewModel>, LandingView>();

		return services;
	}
}
