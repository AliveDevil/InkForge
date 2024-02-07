using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Threading;

using InkForge.Common;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ReactiveUI;

using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

static class Program
{
	[STAThread]
	public static void Main(string[] args)
		=> BuildAvaloniaApp()
			.UseMicrosoftExtensionsHosting(args)
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

	private static void OnSetup(this HostApplicationBuilder hostBuilder, AppBuilder appBuilder)
	{
		var dispatcher = Dispatcher.UIThread;

		var app = appBuilder.Instance!;
		hostBuilder.Services
			.AddSingleton(app)
			.AddSingleton(app.ApplicationLifetime!)
			.AddSingleton(app.PlatformSettings!)
			.AddSingleton(dispatcher);

		var host = hostBuilder.Build();
		host.Services.UseMicrosoftDependencyResolver();
		app.SetValue(App.HostProperty, host);
		dispatcher.ShutdownStarted += host.Shutdown;

		dispatcher.Post(static arg =>
		{
			var host = (IHost)arg!;
			host.StartAsync()
				.GetAwaiter()
				.GetResult();
		}, host, DispatcherPriority.Send);
	}

	private static void Shutdown(this IHost host, object? sender, EventArgs e)
		=> host.StopAsync().GetAwaiter().GetResult();

	private static AppBuilder UseMicrosoftExtensionsHosting(this AppBuilder builder, string[] args)
	{
		var hostBuilder = Host.CreateApplicationBuilder(args);
		ConfigureServices(hostBuilder.Services);
		builder.AfterSetup(hostBuilder.OnSetup);
		return builder;
	}
}
