using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;

using InkForge.Desktop.Views;

using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

using Splat;
using Splat.Microsoft.Extensions.DependencyInjection;

namespace InkForge.Desktop;

public partial class App : Application
{
	public static readonly StyledProperty<IServiceProvider> ServiceProviderProperty
		= AvaloniaProperty.Register<App, IServiceProvider>(
			name: nameof(ServiceProvider),
			coerce: OnServiceProviderChanged);

	public IServiceProvider ServiceProvider => GetValue(ServiceProviderProperty);

	public static void Configure(IServiceCollection services)
	{
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
		_ = ApplicationLifetime switch
		{
			IClassicDesktopStyleApplicationLifetime desktop => desktop.MainWindow = new MainWindow(),
			_ => throw new NotSupportedException(),
		};
	}

	private static IServiceProvider OnServiceProviderChanged(AvaloniaObject @object, IServiceProvider provider)
	{
		provider.UseMicrosoftDependencyResolver();
		return provider;
	}
}
