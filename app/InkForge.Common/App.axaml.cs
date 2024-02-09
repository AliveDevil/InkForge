using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using InkForge.Common.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

namespace InkForge.Common;

public partial class App : Application
{
	public static readonly StyledProperty<IServiceProvider> ServiceProviderProperty = AvaloniaProperty.Register<App, IServiceProvider>(nameof(ServiceProvider));

	public IServiceProvider ServiceProvider => GetValue(ServiceProviderProperty);

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
}
