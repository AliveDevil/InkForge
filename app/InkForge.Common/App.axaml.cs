using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using InkForge.Common.ViewModels;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

using ReactiveUI;

using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace InkForge.Common;

public partial class App : Application
{
	public static readonly StyledProperty<IServiceProvider> ServiceProviderProperty
		= AvaloniaProperty.Register<App, IServiceProvider>(
			name: nameof(ServiceProvider),
			coerce: OnServiceProviderChanged);

	public IServiceProvider ServiceProvider => GetValue(ServiceProviderProperty);

	public static void Configure(IServiceCollection services, IConfigurationManager configuration)
	{
		configuration.SetBasePath(AppContext.BaseDirectory);
		configuration.AddJsonFile(
			new ManifestEmbeddedFileProvider(typeof(App).Assembly),
			"Properties/Settings.json", false, false);
		configuration.AddJsonFile(
			Path.Combine(
				Environment.GetFolderPath(
					Environment.SpecialFolder.ApplicationData,
					Environment.SpecialFolderOption.DoNotVerify),
				"InkForge",
				"UserSettings.json"), true, true);
		configuration.AddJsonFile("Settings.json", true, true);

		services.UseMicrosoftDependencyResolver();
		Locator.CurrentMutable.InitializeSplat();
		Locator.CurrentMutable.InitializeReactiveUI();

		services.AddInkForge();
	}

	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		var viewModel = ActivatorUtilities.GetServiceOrCreateInstance<AppViewModel>(ServiceProvider);
		var view = ViewLocator.Current.ResolveView(viewModel)!;
		view.ViewModel = viewModel;
		_ = ApplicationLifetime switch
		{
			IClassicDesktopStyleApplicationLifetime desktop => desktop.MainWindow = view as Window,
			ISingleViewApplicationLifetime singleView => singleView.MainView = view as Control,
			_ => throw new NotSupportedException(),
		};

		base.OnFrameworkInitializationCompleted();
	}

	private static IServiceProvider OnServiceProviderChanged(AvaloniaObject @object, IServiceProvider provider)
	{
		provider.UseMicrosoftDependencyResolver();
		return provider;
	}
}
