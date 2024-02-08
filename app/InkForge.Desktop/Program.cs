using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Threading;

using InkForge.Common;

using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

static class Program
{
	[STAThread]
	public static void Main(string[] args)
		=> BuildAvaloniaApp()
			.UseMicrosoftDependencyInjection()
			.StartWithClassicDesktopLifetime(args);

	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.UseReactiveUI()
			.LogToTrace();

	private static void ConfigureServices(IServiceCollection services)
	{
		services.UseMicrosoftDependencyResolver();
		var mutableResolver = Locator.CurrentMutable;
		mutableResolver.InitializeSplat();
		mutableResolver.InitializeReactiveUI();

		services.AddHttpClient();

		// services.UseFactories();
		// services.AddViewModelFactory();
		// services.AddTransient<IViewFor<MainViewModel>, MainWindow>();
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

		var serviceProvider = services.BuildServiceProvider();
		serviceProvider.UseMicrosoftDependencyResolver();
		app.SetValue(App.ServiceProviderProperty, serviceProvider);
		dispatcher.ShutdownFinished += (_, _) => serviceProvider.Dispose();
	}

	private static AppBuilder UseMicrosoftDependencyInjection(this AppBuilder builder)
	{
		ServiceCollection services = [];
		ConfigureServices(services);
		builder.AfterSetup(services.OnSetup);
		return builder;
	}
}
