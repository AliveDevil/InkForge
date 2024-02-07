using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using Microsoft.Extensions.Hosting;

using ReactiveUI;

namespace InkForge.Common;

public partial class App : Application
{
	public static readonly StyledProperty<IHost> HostProperty = AvaloniaProperty.Register<App, IHost>("Host");

	public IHost Host => GetValue(HostProperty);

	public IServiceProvider Services => Host.Services;

	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		// var viewModel = Services.Activate<MainViewModel>();
		// var view = ViewLocator.Current.ResolveView(viewModel);
		// switch (ApplicationLifetime)
		// {
		// 	case IClassicDesktopStyleApplicationLifetime desktop:
		// 		desktop.MainWindow = view as Window;
		// 		break;

		// 	case ISingleViewApplicationLifetime singleView:
		// 		singleView.MainView = view as Control;
		// 		break;

		// 	default:
		// 		throw new NotSupportedException();
		// }

		base.OnFrameworkInitializationCompleted();
	}
}
