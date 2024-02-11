using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Avalonia.Threading;

using InkForge.Common;
using InkForge.Common.ViewModels;
using InkForge.Desktop.Views;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

static class Program
{
	private static readonly ConfigurationManager Configuration = new();

	[STAThread]
	public static void Main(string[] args)
		=> BuildAvaloniaApp()
			.UseMicrosoftDependencyInjection()
			.StartWithClassicDesktopLifetime(args, WithMicrosoftDependencyInjection);

	private static void WithMicrosoftDependencyInjection(IClassicDesktopStyleApplicationLifetime lifetime)
	{
		Configuration.AddCommandLine(lifetime.Args ?? []);
	}

	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.UseReactiveUI()
			.WithInterFont()
			.LogToTrace();

	private static void ConfigureServices(IServiceCollection services)
	{
		services.AddTransient<IViewFor<AppViewModel>, MainWindow>();
	}

	private static void OnSetup(this IServiceCollection services, AppBuilder appBuilder)
	{
		var dispatcher = Dispatcher.UIThread;
		var app = appBuilder.Instance!;
		services
			.AddSingleton(app)
			.AddSingleton(app.ApplicationLifetime!)
			.AddSingleton(app.PlatformSettings!)
			.AddSingleton(dispatcher);

		ConfigureServices(services);

		var serviceProvider = services.BuildServiceProvider();
		app.SetValue(App.ServiceProviderProperty, serviceProvider);
		dispatcher.ShutdownFinished += (_, _) => serviceProvider.Dispose();
	}

	private static AppBuilder UseMicrosoftDependencyInjection(this AppBuilder builder)
	{
		ServiceCollection services = [];
		App.Configure(services, Configuration);

		builder.AfterSetup(services.OnSetup);
		return builder;
	}
}
