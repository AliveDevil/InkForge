using InkForge.Common.Controllers;
using InkForge.Common.Data;
using InkForge.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static class InkForgeServiceCollections
{
	public static IServiceCollection AddInkForge(this IServiceCollection services)
	{
		services.AddHttpClient();

		services.AddDbContextFactory<NoteDbContext, NoteDbContextFactory>();

		services.AddSingleton<WorkspaceController>();

		return services;
	}
}
