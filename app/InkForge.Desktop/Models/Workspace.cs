using Microsoft.Extensions.DependencyInjection;

namespace InkForge.Desktop.Models;

public class Workspace(IServiceScope scope)
{
	public IServiceProvider ServiceProvider => scope.ServiceProvider;
}
